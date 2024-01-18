using UptecTodoApi.Models;

namespace UptecTodoApi.Services
{
    public class TodoService : ITodoService
    {
        private static readonly List<TodoModel> _todoList = new List<TodoModel>();
        public Task<TodoDto> CreateAsync(TodoDto dto)
        {
            _todoList.Add(ConvertToModel(dto));
            return Task.FromResult(dto);
        }

        public Task<TodoDto> UpdateAsync(TodoDto dto)
        {
            var model = _todoList.FirstOrDefault(x => x.Id == dto.Id);
            if (model == null)
                throw new Exception("Todo item not found");
            model.Title = dto.Title;
            model.Description = dto.Description;
            return Task.FromResult(dto);
        }

        public Task DeleteAsync(Guid id)
        {
            var model = _todoList.FirstOrDefault(x => x.Id == id);
            if (model == null)
                throw new Exception("Todo item not found");
            _todoList.Remove(model);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<TodoDto>> GetAllAsync()
        {
            return Task.FromResult(_todoList.Select(ConvertFromModel));
        }

        public Task<TodoDto> GetAsync(Guid id)
        {
            var model = _todoList.FirstOrDefault(x => x.Id == id);
            if (model == null)
                throw new Exception("Todo item not found");
            return Task.FromResult(ConvertFromModel(model));
        }


        #region Private Methods
        private TodoModel ConvertToModel(TodoDto dto) => new TodoModel
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description
        };

        private TodoDto ConvertFromModel(TodoModel model) => new TodoDto
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description
        };
        #endregion
    }
}
