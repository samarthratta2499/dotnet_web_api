namespace OrgCodeReviewCases.TC019;

using Microsoft.AspNetCore.Mvc;

public sealed record User(string DisplayName, string CitizenId, string PasswordHash, string RefreshToken, string InternalUserId);

public sealed record OrgApiResponse<T>(bool Success, string Code, string Message, T Data, string TraceId)
{
    public static OrgApiResponse<T> Ok(T data, string traceId) => new(true, "SUCCESS", "Success", data, traceId);
}

[ApiController]
[Route("api/org/v{version:apiVersion}/users")]
public sealed class UsersController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var user = new User("Demo", "1234567890123", "hash", "refresh-token", "internal-001");
        var responseData = new { user.DisplayName, user.CitizenId, user.PasswordHash, user.RefreshToken, user.InternalUserId };
        var traceId = HttpContext.TraceIdentifier;
        return Ok(OrgApiResponse<object>.Ok(responseData, traceId));
    }
}
