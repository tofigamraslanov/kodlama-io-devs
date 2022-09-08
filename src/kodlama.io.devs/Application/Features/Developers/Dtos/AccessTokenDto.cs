using Core.Security.JWT;

namespace Application.Features.Users.Dtos;

public class AccessTokenDto
{
    public AccessToken AccessToken { get; set; } = null!;
}