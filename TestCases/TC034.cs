namespace OrgCodeReviewCases.TC034;

public interface IService
{
    Task<UserDto> GetUserAsync(string id, CancellationToken ct);
}

public sealed record UserDto(string Id = "");

public sealed class Example
{
    private readonly IService _service;

    public Example(IService service) => _service = service;

    public async Task<UserDto> GetUserAsync(string userId, CancellationToken ct)
        => await _service.GetUserAsync(userId, ct);
}
