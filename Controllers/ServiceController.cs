using Microsoft.AspNetCore.Mvc;

namespace dotnet_web_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IService _service;

    public ServiceController(IService service)
    {
        _service = service;
    }

}
