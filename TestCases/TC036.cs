namespace OrgCodeReviewCases.TC036;

public interface IUserRepository { }

public sealed class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) => _userRepository = userRepository;
}
