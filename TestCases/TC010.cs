namespace OrgCodeReviewCases.TC010;

using Microsoft.AspNetCore.Mvc;

public sealed record PaymentRequest(decimal Amount);

public interface IPaymentService
{
    Task<object> PayAsync(PaymentRequest request, CancellationToken ct);
}

public interface IIdempotencyService
{
    Task ValidateAsync(string key, CancellationToken ct);
}

public sealed record OrgApiResponse<T>(bool Success, string Code, string Message, T Data, string TraceId)
{
    public static OrgApiResponse<T> Ok(T data, string traceId) => new(true, "SUCCESS", "Success", data, traceId);
}

[ApiController]
[Route("api/org/v{version:apiVersion}/payments")]
public sealed class PaymentsController : ControllerBase
{
    private readonly IPaymentService _payment;
    private readonly IIdempotencyService _idempotency;

    public PaymentsController(IPaymentService payment, IIdempotencyService idempotency)
    {
        _payment = payment;
        _idempotency = idempotency;
    }

    [HttpPost]
    public async Task<IActionResult> PayAsync(PaymentRequest request, CancellationToken ct)
    {
        var key = Request.Headers["X-Org-Idempotency-Key"].ToString();
        await _idempotency.ValidateAsync(key, ct);
        var result = await _payment.PayAsync(request, ct);
        var traceId = HttpContext.TraceIdentifier;
        return Ok(OrgApiResponse<object>.Ok(result, traceId));
    }
}
