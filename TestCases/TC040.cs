namespace OrgCodeReviewCases.TC040;

using Microsoft.Extensions.Logging;

public interface IService
{
    Task ProcessAsync(CancellationToken ct);
}

public sealed class Example
{
    private readonly IService _service;
    private readonly ILogger<Example> _logger;

    public Example(IService service, ILogger<Example> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task ProcessAsync(CancellationToken ct)
    {
        try
        {
            await _service.ProcessAsync(ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{EventName}", "ProcessFailed");
            throw;
        }
    }
}
