namespace OrgCodeReviewCases.TC039;

public interface IService
{
    Task ProcessAsync(CancellationToken ct);
}

public sealed class Example
{
    private readonly IService _service;

    public Example(IService service) => _service = service;

    public async Task ProcessAsync(CancellationToken ct)
    {
        try
        {
            await _service.ProcessAsync(ct);
        }
        catch (Exception) { }
    }
}
