namespace FrontEnd.Services;

public interface IToDoService
{
    Task CreateAsync(TodoDto dto);
    Task EditAsync(TodoDto dto);
    Task<TodoDto?> GetAsync(int id);
    Task<IList<TodoDto>?> GetAllAsync();
    Task DeleteAsync(int id);
}