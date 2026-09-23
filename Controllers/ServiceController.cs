using Microsoft.AspNetCore.Mvc;

namespace dotnet_web_api.Controllers;

[ApiController]
[Route("api/org/v{version:apiVersion}/users")]
public class UsersController : ControllerBase
{
    private readonly IUserApplicationService _service;

    public UsersController(
        IUserApplicationService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(
        string id,
        CancellationToken ct)
    {
        var user = await _service.GetAsync(id, ct);

        var traceId =
            Activity.Current?.TraceId.ToString()
            ?? HttpContext.TraceIdentifier;

        return Ok(OrgApiResponse<UserDto>.Success(user, traceId));
    }
}
