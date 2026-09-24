namespace OrgCodeReviewCases.TC029;

public sealed record UserDto(string Id);

public interface IUserRepository
{
    Task<UserDto> GetAsync(string id);
}

public sealed class UserApplicationService
{
    private readonly IUserRepository _repository;

    public UserApplicationService(IUserRepository repository) => _repository = repository;

    public async Task<UserDto> GetAsync(string id) => await _repository.GetAsync(id);
}
