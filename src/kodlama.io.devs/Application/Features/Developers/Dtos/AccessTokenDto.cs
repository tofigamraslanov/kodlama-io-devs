using Core.Security.JWT;

namespace Application.Features.Developers.Dtos;

public class AccessTokenDto
{
    public AccessToken AccessToken { get; set; } = null!;
}