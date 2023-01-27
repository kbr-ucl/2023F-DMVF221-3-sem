var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/hello", () =>
{
    return "Hello World";
});

app.MapGet("/hello/{name}", (string name) =>
{
    return $"Hello World: {name}";
});

app.Run();

