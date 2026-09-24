namespace OrgCodeReviewCases.TC024;

using Microsoft.AspNetCore.Mvc;

public interface IUserApplicationService
{
    Task<object?> FindAsync(string id, CancellationToken ct);
}

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
    public async Task<IActionResult> GetAsync(string id, CancellationToken ct)
    {
        var result = await _service.FindAsync(id, ct);
        var traceId = HttpContext.TraceIdentifier;
        return Ok(OrgApiResponse<object>.Ok(result, traceId));
    }
}
