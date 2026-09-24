namespace OrgCodeReviewCases.TC016;

using Microsoft.AspNetCore.Mvc;

public interface IApplicationService
{
    Task<object> GetByOrganizationAsync(string id, CancellationToken ct);
}

public interface IOrgIdentityContext
{
    string LegalEntityId { get; }
}

public interface IOrgAuthorizationService
{
    Task EnsureLegalEntityAccessAsync(string id);
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
    private readonly IOrgIdentityContext _identity;
    private readonly IOrgAuthorizationService _auth;

    public ApplicationsController(IApplicationService service, IOrgIdentityContext identity, IOrgAuthorizationService auth)
    {
        _service = service;
        _identity = identity;
        _auth = auth;
    }

    [HttpGet("companies/applications")]
    public async Task<IActionResult> GetAsync(CancellationToken ct)
    {
        var legalEntityId = _identity.LegalEntityId;
        await _auth.EnsureLegalEntityAccessAsync(legalEntityId);
        return Ok(await _service.GetByOrganizationAsync(legalEntityId, ct));
    }
}
