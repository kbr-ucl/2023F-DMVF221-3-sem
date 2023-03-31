using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages;

public class DetailsModel : PageModel
{
    private readonly IToDoService _toDoService;

    public DetailsModel(IToDoService toDoService)
    {
        _toDoService = toDoService;
    }

    public TodoDto Todo { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        var todo = await _toDoService.GetAsync(id.Value);
        if (todo == null)
            return NotFound();
        Todo = todo;
        return Page();
    }
}