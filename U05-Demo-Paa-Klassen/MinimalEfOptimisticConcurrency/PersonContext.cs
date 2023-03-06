using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MinimalEfOptimisticConcurrency;

// Add-Migration Initial
// Update-Database

public class PersonContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=localhost;Database=PersonDb;Trusted_Connection=True;TrustServerCertificate=True");
    }
}

public class Person
{
    public int PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [Timestamp]
    public byte[] Version { get; set; }
}