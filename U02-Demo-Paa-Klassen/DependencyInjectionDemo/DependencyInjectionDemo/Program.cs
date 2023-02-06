var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IHelloService, HelloService>();


var app = builder.Build();

app.MapGet("/hello", (IHelloService helloService) =>
{
    return helloService.SayHello();
    //return "Hello World";
});

app.MapGet("/hello/{name}", (IHelloService helloService, string name) =>
{
    return helloService.SayHello(name);
    //return $"Hello World: {name}";
});

app.Run();


public interface IHelloService
{
    string SayHello();
    string SayHello(string name);
}


public class HelloService : IHelloService
{
    string IHelloService.SayHello()
    {
        return "Hello World";
    }

    string IHelloService.SayHello(string name)
    {
        return $"Hello World: {name}";
    }

}