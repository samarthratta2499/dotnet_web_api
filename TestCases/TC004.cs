namespace OrgCodeReviewCases.TC004;

using Microsoft.AspNetCore.Mvc;

public sealed record OrgApiError(bool Success, string Code, string Message, string TraceId);

public sealed record OrgApiResponse<T>(bool Success, string Code, string Message, T Data, string TraceId)
{
    public static OrgApiResponse<T> Ok(T data, string traceId) => new(true, "SUCCESS", "Success", data, traceId);
}

[ApiController]
[Route("api/org/v{version:apiVersion}/users")]
public sealed class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(string name)
    {
        var traceId = HttpContext.TraceIdentifier;
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest(new OrgApiError(false, "BIZ-USER-001", "Invalid user data", traceId));
        return Ok(OrgApiResponse<object>.Ok(new { created = true }, traceId));
    }
}
