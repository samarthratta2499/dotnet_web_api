namespace OrgCodeReviewCases.TC038;

public sealed record UserDto;

public sealed class UserService
{
    public UserDto GetUser(string userId) => new();
}
