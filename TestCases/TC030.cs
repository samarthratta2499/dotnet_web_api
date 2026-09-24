namespace OrgCodeReviewCases.TC030;

public sealed record UserDto(string Id);

public interface IUserRepository
{
    Task<UserDto> GetAsync(string id, CancellationToken ct);
}

public sealed class UserApplicationService
{
    private readonly IUserRepository _repository;

    public UserApplicationService(IUserRepository repository) => _repository = repository;

    public async Task<UserDto> GetAsync(string id, CancellationToken cancellationToken)
        => await _repository.GetAsync(id, cancellationToken);
}
