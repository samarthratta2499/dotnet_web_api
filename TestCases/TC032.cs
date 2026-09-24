namespace OrgCodeReviewCases.TC032;

public interface IService
{
    UserDto Get(string id);
}

public sealed record UserDto(string Id = "");

public sealed class Example
{
    private readonly IService _service;

    public Example(IService service) => _service = service;

    public UserDto GetUser(string userId)
    {
        var userProfile = _service.Get(userId);
        return userProfile;
    }
}
