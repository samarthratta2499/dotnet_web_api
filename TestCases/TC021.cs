namespace OrgCodeReviewCases.TC021;

using Microsoft.AspNetCore.Mvc;

public sealed record OrderRequest(decimal Amount);

public interface IOrderService
{
    Task<object> CreateAsync(OrderRequest request, decimal total, CancellationToken ct);
}

public sealed record OrgApiResponse<T>(bool Success, string Code, string Message, T Data, string TraceId)
{
    public static OrgApiResponse<T> Ok(T data, string traceId) => new(true, "SUCCESS", "Success", data, traceId);
}

[ApiController]
[Route("api/org/v{version:apiVersion}/orders")]
public sealed class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> CreateAsync(OrderRequest request, CancellationToken ct)
    {
        var total = request.Amount > 10000 ? request.Amount * 0.9m : request.Amount;
        var result = await _service.CreateAsync(request, total, ct);
        var traceId = HttpContext.TraceIdentifier;
        return Ok(OrgApiResponse<object>.Ok(result, traceId));
    }
}
