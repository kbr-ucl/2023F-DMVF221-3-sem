using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPageValidation.Data;

namespace RazorPageValidation.Pages.Book;

public class EditModel : PageModel
{
    private readonly BookContext _context;

    public EditModel(BookContext context)
    {
        _context = context;
    }

    [BindProperty] public BookModel BookModel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null || _context.BookModel == null) return NotFound();

        var bookmodel = await _context.BookModel.FirstOrDefaultAsync(m => m.ISBN == id);
        if (bookmodel == null) return NotFound();
        BookModel = bookmodel;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        _context.Attach(BookModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookModelExists(BookModel.ISBN))
                return NotFound();
            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool BookModelExists(string id)
    {
        return (_context.BookModel?.Any(e => e.ISBN == id)).GetValueOrDefault();
    }
}