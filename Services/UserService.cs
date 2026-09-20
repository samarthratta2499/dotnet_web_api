public class UserService
{
    public async Task<UserDto> GetUser(int id)
    {
        return await _userService.GetByIdAsync(id);
    }
}