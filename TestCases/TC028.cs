namespace OrgCodeReviewCases.TC028;

using Microsoft.AspNetCore.Mvc;

public interface IExternalGateway
{
    Task<string> GetProfileAsync(string id, CancellationToken ct);
}

public sealed record OrgApiResponse<T>(bool Success, string Code, string Message, T Data, string TraceId)
{
    public static OrgApiResponse<T> Ok(T data, string traceId) => new(true, "SUCCESS", "Success", data, traceId);
}

[ApiController]
[Route("api/org/v{version:apiVersion}/profiles")]
public sealed class ProfilesController : ControllerBase
{
    private readonly IExternalGateway _gateway;

    public ProfilesController(IExternalGateway gateway) => _gateway = gateway;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfileAsync(string id, CancellationToken ct)
    {
        var result = await _gateway.GetProfileAsync(id, ct);
        var traceId = HttpContext.TraceIdentifier;
        return Ok(OrgApiResponse<object>.Ok(result, traceId));
    }
}
