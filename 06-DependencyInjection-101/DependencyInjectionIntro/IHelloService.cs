public interface IHelloService
{
    string SayHello();
    string SayHello(string name);
}


public class HelloService : IHelloService
{
    public string SayHello()
    {
        return "Hello World";
    }

    public string SayHello(string name)
    {
        return $"Hello World: {name}";
    }
}