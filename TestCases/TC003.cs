namespace OrgCodeReviewCases.TC003;

using Microsoft.AspNetCore.Mvc;

public sealed record OrgApiError(bool Success, string Code, string Message, string TraceId);

[ApiController]
[Route("api/org/v{version:apiVersion}/users")]
public sealed class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(string name)
    {
        var traceId = HttpContext.TraceIdentifier;
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest(new OrgApiError(false, "INVALID", "Invalid user data", traceId)); // TC-003
        return Ok();
    }
}
