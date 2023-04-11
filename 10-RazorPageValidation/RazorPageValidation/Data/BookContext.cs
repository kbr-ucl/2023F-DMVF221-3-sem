using Microsoft.EntityFrameworkCore;

namespace RazorPageValidation.Data
{
    public class BookContext : DbContext
    {
        public BookContext (DbContextOptions<BookContext> options)
            : base(options)
        {
        }

        public DbSet<BookModel> BookModel { get; set; } = default!;
    }
}
