using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageValidation.Data;

namespace RazorPageValidation.Pages.Book;

public class CreateModel : PageModel
{
    private readonly BookContext _context;

    public CreateModel(BookContext context)
    {
        _context = context;
    }

    [BindProperty] public BookModel BookModel { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }


    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || _context.BookModel == null || BookModel == null) return Page();

        _context.BookModel.Add(BookModel);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}