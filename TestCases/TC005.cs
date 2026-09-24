namespace OrgCodeReviewCases.TC005;

using Microsoft.AspNetCore.Mvc;

public sealed record OrgApiResponse<T>(bool Success, string Code, string Message, T Data, string TraceId)
{
    public static OrgApiResponse<T> Ok(T data, string traceId) => new(true, "SUCCESS", "Success", data, traceId);
}

[ApiController]
[Route("api/org/v{version:apiVersion}/users")]
public sealed class UsersController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        var data = new { id, displayName = "Demo User" };
        var traceId = string.Empty;
        return Ok(OrgApiResponse<object>.Ok(data, traceId));
    }
}
