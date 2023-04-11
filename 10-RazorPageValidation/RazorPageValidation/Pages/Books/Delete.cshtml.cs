using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPageValidation.Data;

namespace RazorPageValidation.Pages.Book;

public class DeleteModel : PageModel
{
    private readonly BookContext _context;

    public DeleteModel(BookContext context)
    {
        _context = context;
    }

    [BindProperty] public BookModel BookModel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null || _context.BookModel == null) return NotFound();

        var bookmodel = await _context.BookModel.FirstOrDefaultAsync(m => m.ISBN == id);

        if (bookmodel == null)
            return NotFound();
        BookModel = bookmodel;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (id == null || _context.BookModel == null) return NotFound();
        var bookmodel = await _context.BookModel.FindAsync(id);

        if (bookmodel != null)
        {
            BookModel = bookmodel;
            _context.BookModel.Remove(BookModel);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}