namespace OrgCodeReviewCases.TC023;

using Microsoft.AspNetCore.Mvc;

public interface IUserRepository
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
    private readonly IUserRepository _repository;

    public UsersController(IUserRepository repository) => _repository = repository;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id, CancellationToken ct)
    {
        var result = await _repository.FindAsync(id, ct);
        var traceId = HttpContext.TraceIdentifier;
        return Ok(OrgApiResponse<object>.Ok(result, traceId));
    }
}
