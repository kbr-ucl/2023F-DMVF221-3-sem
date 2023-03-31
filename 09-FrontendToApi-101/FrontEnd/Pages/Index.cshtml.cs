using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages;

public class IndexModel : PageModel
{
    private readonly IToDoService _toDoService;

    public IndexModel(IToDoService toDoService)
    {
        _toDoService = toDoService;
    }

    public IList<TodoDto> Todo { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Todo = await _toDoService.GetAllAsync();
    }
}