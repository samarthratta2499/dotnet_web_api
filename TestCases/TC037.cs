namespace OrgCodeReviewCases.TC037;

public sealed record UserDto;

public sealed class userService
{
    public UserDto getUser(string userId) => new();
}
