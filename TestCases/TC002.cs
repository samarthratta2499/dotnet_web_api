namespace OrgCodeReviewCases.TC002;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public interface IUserApplicationService
{
    Task<UserDto> GetAsync(string id, CancellationToken ct);
}

public sealed record UserDto(string Id, string DisplayName);

public sealed record OrgApiResponse<T>(bool Success, string Code, string Message, T Data, string TraceId)
{
    public static OrgApiResponse<T> Ok(T data, string traceId) => new(true, "SUCCESS", "Success", data, traceId);
}

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
        var traceId = Activity.Current?.TraceId.ToString() ?? HttpContext.TraceIdentifier;
        return Ok(OrgApiResponse<UserDto>.Ok(user, traceId));
    }
}
