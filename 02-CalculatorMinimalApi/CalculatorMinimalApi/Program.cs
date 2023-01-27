var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("hello", () =>
{
    return $"Calculator: use add/1/3 for adding 1 + 3";
});

app.MapGet("/add/{a}/{b}", (int a, int b) =>
{
    return $"{a + b}";
});

app.Run();

