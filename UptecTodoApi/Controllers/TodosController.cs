using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using UptecTodoApi.Models;
using UptecTodoApi.Services;

namespace UptecTodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        #region Read Access
        [HttpGet("{id:guid}")]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:Read")]
        public async Task<IActionResult> Get(Guid id)
        {
            var dto = await _todoService.GetAsync(id);
            return Ok(dto);
        }

        [HttpGet]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:Read")]
        public async Task<IActionResult> Get()
        {
            var dtoList = await _todoService.GetAllAsync();
            return Ok(dtoList);
        }
        #endregion

        #region Write Access
        [HttpPost]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:Write")]
        public async Task<IActionResult> Post([FromBody] CreateTodoRequest request)
        {
            if (ModelState.IsValid)
            {
                var dto = await _todoService.CreateAsync(new TodoDto
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title,
                    Description = request.Description
                });
                return Ok(dto);
            }
            return BadRequest();
        }

        [HttpPut]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:Write")]
        public async Task<IActionResult> Put([FromBody] UpdateTodoRequest request)
        {
            if (ModelState.IsValid)
            {
                var dto = await _todoService.UpdateAsync(new TodoDto
                {
                    Id = request.Id,
                    Title = request.Title,
                    Description = request.Description
                });
                return Ok(dto);
            }
            return BadRequest();
        }

        [HttpDelete("{id:guid}")]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:Write")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _todoService.DeleteAsync(id);
            return NoContent();
        }


        #endregion
    }
}
