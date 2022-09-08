using Application.Features.Users.Dtos;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Developers.Commands.LoginDeveloper;

public record LoginDeveloperCommand(UserForLoginDto UserForLoginDto) : IRequest<AccessTokenDto>;

public class LoginDeveloperCommandHandler : IRequestHandler<LoginDeveloperCommand, AccessTokenDto>
{
    private readonly IUserRepository _repository;
    private readonly ITokenHelper _tokenHelper;

    public LoginDeveloperCommandHandler(IUserRepository repository, ITokenHelper tokenHelper)
    {
        _repository = repository;
        _tokenHelper = tokenHelper;
    }

    public async Task<AccessTokenDto> Handle(LoginDeveloperCommand request, CancellationToken cancellationToken)
    {
        User? user = await _repository.GetAsync(u => u.Email == request.UserForLoginDto.Email);

        var result =
            HashingHelper.VerifyPasswordHash(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt);

        if (!result)
            throw new Exception("Wrong Credentials");

        AccessToken accessToken = _tokenHelper.CreateToken(user, new List<OperationClaim>());

        return new AccessTokenDto() { AccessToken = accessToken };
    }
}