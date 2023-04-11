using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPageValidation.Data;

namespace RazorPageValidation.Pages.Book;

public class DetailsModel : PageModel
{
    private readonly BookContext _context;

    public DetailsModel(BookContext context)
    {
        _context = context;
    }

    public BookModel BookModel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null || _context.BookModel == null) return NotFound();

        var bookmodel = await _context.BookModel.FirstOrDefaultAsync(m => m.ISBN == id);
        if (bookmodel == null)
            return NotFound();
        BookModel = bookmodel;
        return Page();
    }
}