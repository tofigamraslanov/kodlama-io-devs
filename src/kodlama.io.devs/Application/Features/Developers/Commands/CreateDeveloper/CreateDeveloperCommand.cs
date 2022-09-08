using Application.Features.Users.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;

namespace Application.Features.Developers.Commands.CreateDeveloper;

public record CreateDeveloperCommand(UserForRegisterDto UserForRegisterDto) : IRequest<AccessTokenDto>;

public class CreateDeveloperCommandHandler : IRequestHandler<CreateDeveloperCommand, AccessTokenDto>
{
    private readonly IDeveloperRepository _repository;
    private readonly IMapper _mapper;
    private readonly ITokenHelper _tokenHelper;

    public CreateDeveloperCommandHandler(IDeveloperRepository repository, IMapper mapper, ITokenHelper tokenHelper)
    {
        _repository = repository;
        _mapper = mapper;
        _tokenHelper = tokenHelper;
    }

    public async Task<AccessTokenDto> Handle(CreateDeveloperCommand request, CancellationToken cancellationToken)
    {
        HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out var passwordHash, out var passwordSalt);

        var developer = new Developer
        {
            FirstName = request.UserForRegisterDto.FirstName,
            LastName = request.UserForRegisterDto.LastName,
            Email = request.UserForRegisterDto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            AuthenticatorType = AuthenticatorType.Email,
            Status = true
        };

        User createdUser = await _repository.AddAsync(developer);

        AccessToken accessToken = _tokenHelper.CreateToken(developer, new List<OperationClaim>());

        return new AccessTokenDto() { AccessToken = accessToken };
    }
}