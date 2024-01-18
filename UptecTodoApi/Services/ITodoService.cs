using UptecTodoApi.Models;

namespace UptecTodoApi.Services
{
    public interface ITodoService
    {
        Task<TodoDto> CreateAsync(TodoDto dto);
        Task<TodoDto> UpdateAsync(TodoDto dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<TodoDto>> GetAllAsync();
        Task<TodoDto> GetAsync(Guid id);
    }
}
