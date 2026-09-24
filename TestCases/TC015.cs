namespace OrgCodeReviewCases.TC015;

using Microsoft.AspNetCore.Mvc;

public interface IApplicationService
{
    Task<object> GetByOrganizationAsync(string id, CancellationToken ct);
}

public sealed record OrgApiResponse<T>(bool Success, string Code, string Message, T Data, string TraceId)
{
    public static OrgApiResponse<T> Ok(T data, string traceId) => new(true, "SUCCESS", "Success", data, traceId);
}

[ApiController]
[Route("api/org/v{version:apiVersion}")]
public sealed class ApplicationsController : ControllerBase
{
    private readonly IApplicationService _service;

    public ApplicationsController(IApplicationService service) => _service = service;

    [HttpGet("companies/{organizationId}/applications")]
    public async Task<IActionResult> GetAsync(string organizationId, CancellationToken ct)
    {
        var result = await _service.GetByOrganizationAsync(organizationId, ct);
        var traceId = HttpContext.TraceIdentifier;
        return Ok(OrgApiResponse<object>.Ok(result, traceId));
    }
}
