namespace OrgCodeReviewCases.TC027;

using Microsoft.AspNetCore.Mvc;

public sealed record OrgApiResponse<T>(bool Success, string Code, string Message, T Data, string TraceId)
{
    public static OrgApiResponse<T> Ok(T data, string traceId) => new(true, "SUCCESS", "Success", data, traceId);
}

[ApiController]
[Route("api/org/v{version:apiVersion}/profiles")]
public sealed class ProfilesController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfileAsync(string id)
    {
        using var client = new HttpClient();
        var json = await client.GetStringAsync($"https://partner.example/users/{id}");
        var traceId = HttpContext.TraceIdentifier;
        return Ok(OrgApiResponse<string>.Ok(json, traceId));
    }
}
