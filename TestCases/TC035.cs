namespace OrgCodeReviewCases.TC035;

public interface IUserRepository { }

public sealed class UserService
{
    private readonly IUserRepository UserRepository;

    public UserService(IUserRepository userRepository) => UserRepository = userRepository;
}
