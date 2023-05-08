using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CorsApiDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors]
public class TestController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "GET: Test message";
    }

    [HttpPost]
    public string Post()
    {
        return "POST: Test message";
    }

    [HttpPut]
    public string Put()
    {
        return "PUT: Test message";
    }
}