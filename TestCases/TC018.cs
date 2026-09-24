namespace OrgCodeReviewCases.TC018;

using Microsoft.AspNetCore.Mvc;

public interface IPersonService
{
    Task<object> GetByPersonIdAsync(string id, CancellationToken ct);
}

public interface IOrgIdentityContext
{
    string PersonId { get; }
}

public sealed record OrgApiResponse<T>(bool Success, string Code, string Message, T Data, string TraceId)
{
    public static OrgApiResponse<T> Ok(T data, string traceId) => new(true, "SUCCESS", "Success", data, traceId);
}

[ApiController]
[Route("api/org/v{version:apiVersion}")]
public sealed class PersonsController : ControllerBase
{
    private readonly IPersonService _service;
    private readonly IOrgIdentityContext _identity;

    public PersonsController(IPersonService service, IOrgIdentityContext identity)
    {
        _service = service;
        _identity = identity;
    }

    [HttpGet("persons/profile")]
    public async Task<IActionResult> GetAsync(CancellationToken ct)
    {
        var result = await _service.GetByPersonIdAsync(_identity.PersonId, ct);
        var traceId = HttpContext.TraceIdentifier;
        return Ok(OrgApiResponse<object>.Ok(result, traceId));
    }
}
