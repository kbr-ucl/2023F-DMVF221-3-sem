using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPageValidation.Data;

namespace RazorPageValidation.Pages.Book;

public class IndexModel : PageModel
{
    private readonly BookContext _context;

    public IndexModel(BookContext context)
    {
        _context = context;
    }

    public IList<BookModel> BookModel { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.BookModel != null) BookModel = await _context.BookModel.ToListAsync();
    }
}