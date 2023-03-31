using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages;

public class DeleteModel : PageModel
{
    private readonly IToDoService _toDoService;

    public DeleteModel(IToDoService toDoService)
    {
        _toDoService = toDoService;
    }

    [BindProperty] public TodoDto Todo { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        var todo = await _toDoService.GetAsync(id.Value);

        if (todo == null)
            return NotFound();
        Todo = todo;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Todo.Id == null) return NotFound();
        await _toDoService.DeleteAsync(Todo.Id);


        return RedirectToPage("./Index");
    }
}