namespace OrgCodeReviewCases.TC022;

using Microsoft.AspNetCore.Mvc;

public sealed record OrderRequest(decimal Amount);

public interface IOrderApplicationService
{
    Task<object> CreateAsync(OrderRequest request, CancellationToken ct);
}

public sealed record OrgApiResponse<T>(bool Success, string Code, string Message, T Data, string TraceId)
{
    public static OrgApiResponse<T> Ok(T data, string traceId) => new(true, "SUCCESS", "Success", data, traceId);
}

[ApiController]
[Route("api/org/v{version:apiVersion}/orders")]
public sealed class OrdersController : ControllerBase
{
    private readonly IOrderApplicationService _service;

    public OrdersController(IOrderApplicationService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> CreateAsync(OrderRequest request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        var traceId = HttpContext.TraceIdentifier;
        return Ok(OrgApiResponse<object>.Ok(result, traceId));
    }
}
