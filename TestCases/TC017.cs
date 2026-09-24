namespace OrgCodeReviewCases.TC017;

using Microsoft.AspNetCore.Mvc;

public interface IPersonService
{
    Task<object> GetByCitizenIdAsync(string id, CancellationToken ct);
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

    public PersonsController(IPersonService service) => _service = service;

    [HttpGet("persons/{citizenId}/profile")]
    public async Task<IActionResult> GetAsync(string citizenId, CancellationToken ct)
    {
        var result = await _service.GetByCitizenIdAsync(citizenId, ct);
        var traceId = HttpContext.TraceIdentifier;
        return Ok(OrgApiResponse<object>.Ok(result, traceId));
    }
}
