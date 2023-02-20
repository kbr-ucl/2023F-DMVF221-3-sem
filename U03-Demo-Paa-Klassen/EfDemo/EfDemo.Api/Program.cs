using System.Text.Json;
using System.Text.Json.Serialization;
using EfDemo.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// https://learn.microsoft.com/en-us/ef/core/cli/powershell
// Add-Migration InitialMigration
// Update-Database
builder.Services.AddDbContext<PublicationContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("PublicationsDatabase"));
});


var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/author/{id}/publications", (PublicationContext db, int id) =>
{
    var result = db.Authors.Include(a => a.Articles).
        ThenInclude(a => a.Authors).
        FirstOrDefault(a => a.Id == id);

    // https://learn.microsoft.com/en-us/ef/core/querying/related-data/serialization
    JsonSerializerOptions options = new()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        WriteIndented = true
    };
    return JsonSerializer.Serialize(result, options);

});
app.Run();
