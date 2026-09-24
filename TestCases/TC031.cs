namespace OrgCodeReviewCases.TC031;

public interface IService
{
    UserDto Get(string id);
}

public sealed record UserDto(string Id = "");

public sealed class Example
{
    private readonly IService _service;

    public Example(IService service) => _service = service;

    public UserDto GetUser(string UserId)
    {
        var UserProfile = _service.Get(UserId);
        return UserProfile;
    }
}
