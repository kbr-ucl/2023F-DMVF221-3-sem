using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages;

public class EditModel : PageModel
{
    private readonly IToDoService _toDoService;

    public EditModel(IToDoService toDoService)
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

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            await _toDoService.EditAsync(Todo);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
            return Page();
        }


        return RedirectToPage("./Index");
    }
}