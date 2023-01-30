

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

app.MapGet("/hello/{name}", (string name) =>
{
    return $"Hello World: {name}";
});

app.MapGet("/add/{tal1}/{tal2}", (int tal1, int tal2) =>
{
    //return $"Result: {tal1 + tal2}";
    //var r = new LommeregnerDto();
    //r.Result = tal1 + tal2;
    return new LommeregnerDto{Result = tal1 + tal2};
});

app.Run();

public class LommeregnerDto
{
    public int Result { get; set; }
}
