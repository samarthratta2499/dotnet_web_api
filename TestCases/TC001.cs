namespace OrgCodeReviewCases.TC001;

using Microsoft.AspNetCore.Mvc;

public interface IUserApplicationService
{
    Task<UserDto> GetAsync(string id, CancellationToken ct);
}

public sealed record UserDto(string Id, string DisplayName);

[ApiController]
[Route("api/org/v{version:apiVersion}/users")]
public sealed class UsersController : ControllerBase
{
    private readonly IUserApplicationService _service;

    public UsersController(IUserApplicationService service) => _service = service;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id, CancellationToken ct)
    {
        var user = await _service.GetAsync(id, ct);
        return Ok(user);
    }
}
