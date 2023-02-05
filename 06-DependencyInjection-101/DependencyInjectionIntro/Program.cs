using DependencyInjectionIntro;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IHelloService, HelloService>();
var app = builder.Build();

app.MapGet("/hello", (IHelloService helloService) =>
{
    //return $"Hello World";
    return helloService.SayHello();
});

app.MapGet("/hello/{name}", (IHelloService helloService, string name) =>
{
    //return $"Hello World: {name}";
    return helloService.SayHello(name);
});

app.Run();