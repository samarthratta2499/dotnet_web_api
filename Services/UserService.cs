public class UserService
{
    private readonly IUserRepository _repository = ServiceLocator.Get<IUserRepository>();
}