namespace OrgCodeReviewCases.TC020;

using Microsoft.AspNetCore.Mvc;

public sealed record User(string DisplayName, string CitizenId);

public sealed record UserPublicDto(string DisplayName, string CitizenIdMasked);

public static class OrgDataMasker
{
    public static string MaskCitizenId(string v)
        => new string('*', Math.Max(0, v.Length - 4)) + v[^Math.Min(4, v.Length)..];
}

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
        var user = new User("Demo", "1234567890123");
        var responseData = new UserPublicDto(user.DisplayName, OrgDataMasker.MaskCitizenId(user.CitizenId));
        var traceId = HttpContext.TraceIdentifier;
        return Ok(OrgApiResponse<object>.Ok(responseData, traceId));
    }
}
