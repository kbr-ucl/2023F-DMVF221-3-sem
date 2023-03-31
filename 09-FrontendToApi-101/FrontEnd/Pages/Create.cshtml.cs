using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages;

public class CreateModel : PageModel
{
    private readonly IToDoService _toDoService;

    public CreateModel(IToDoService toDoService)
    {
        _toDoService = toDoService;
    }

    [BindProperty] public TodoDto Todo { get; set; } = default!;

    public IActionResult OnGet()
    {
        Todo = new TodoDto {Version = new byte[1]};
        return Page();
    }


    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || Todo == null) return Page();

        await _toDoService.CreateAsync(Todo);

        return RedirectToPage("./Index");
    }
}