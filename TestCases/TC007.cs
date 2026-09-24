namespace OrgCodeReviewCases.TC007;

using Microsoft.AspNetCore.Mvc;

public sealed record OrgApiResponse<T>(bool Success, string Code, string Message, T Data, string TraceId)
{
    public static OrgApiResponse<T> Ok(T data, string traceId) => new(true, "SUCCESS", "Success", data, traceId);
}

[ApiController]
[Route("api/GetUserProfiles")]
public sealed class UserProfilesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var traceId = HttpContext.TraceIdentifier;
        return Ok(OrgApiResponse<object>.Ok(new { status = "ok" }, traceId));
    }
}
