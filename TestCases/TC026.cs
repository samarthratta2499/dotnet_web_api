namespace OrgCodeReviewCases.TC026;

using Microsoft.AspNetCore.Mvc;

public sealed record UserEntity(string Id, string DisplayName);

public sealed record UserResponseDto(string Id, string DisplayName);

public interface IUserService
{
    Task<UserEntity> GetEntityAsync(string id, CancellationToken ct);
}

[ApiController]
[Route("api/org/v{version:apiVersion}/users")]
public sealed class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service) => _service = service;

    [HttpGet("{id}")]
    public async Task<UserResponseDto> GetAsync(string id, CancellationToken ct)
    {
        var e = await _service.GetEntityAsync(id, ct);
        return new UserResponseDto(e.Id, e.DisplayName);
    }
}
