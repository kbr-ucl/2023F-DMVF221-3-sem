namespace FrontEnd.Services;

public class ToDoServiceProxy : IToDoService
{
    private readonly HttpClient _api;

    public ToDoServiceProxy(HttpClient api)
    {
        _api = api;
    }

    async Task IToDoService.CreateAsync(TodoDto dto)
    {
        var response = await _api.PostAsJsonAsync("api/ToDo", dto);

        if (response.IsSuccessStatusCode) return;

        var message = await response.Content.ReadAsStringAsync();
        throw new Exception(message);
    }

    async Task IToDoService.DeleteAsync(int id)
    {
        var response = await _api.DeleteAsync($"api/ToDo/{id}");

        if (response.IsSuccessStatusCode) return;

        var message = await response.Content.ReadAsStringAsync();
        throw new Exception(message);
    }

    async Task IToDoService.EditAsync(TodoDto dto)
    {
        var response = await _api.PutAsJsonAsync("api/ToDo", dto);

        if (response.IsSuccessStatusCode) return;

        var messages = await response.Content.ReadAsStringAsync();
        throw new Exception(messages);
    }

    async Task<TodoDto?> IToDoService.GetAsync(int id)
    {
        return await _api.GetFromJsonAsync<TodoDto>($"api/ToDo/{id}");
    }

    async Task<IList<TodoDto>?> IToDoService.GetAllAsync()
    {
        return await _api.GetFromJsonAsync<List<TodoDto>>("api/ToDo");
    }
}