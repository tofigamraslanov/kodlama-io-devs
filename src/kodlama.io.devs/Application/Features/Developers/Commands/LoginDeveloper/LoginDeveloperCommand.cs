using Application.Features.Developers.Dtos;
using Application.Features.Developers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;

namespace Application.Features.Developers.Commands.LoginDeveloper;

public record LoginDeveloperCommand(UserForLoginDto UserForLoginDto) : IRequest<AccessTokenDto>;

public class LoginDeveloperCommandHandler : IRequestHandler<LoginDeveloperCommand, AccessTokenDto>
{
    private readonly IDeveloperRepository _repository;
    private readonly ITokenHelper _tokenHelper;
    private readonly DeveloperBusinessRules _businessRules;
    private readonly IMapper _mapper;

    public LoginDeveloperCommandHandler(IDeveloperRepository repository, ITokenHelper tokenHelper, DeveloperBusinessRules businessRules,
        IMapper mapper)
    {
        _repository = repository;
        _tokenHelper = tokenHelper;
        _businessRules = businessRules;
        _mapper = mapper;
    }

    public async Task<AccessTokenDto> Handle(LoginDeveloperCommand request, CancellationToken cancellationToken)
    {
        Developer? developer = await _repository.GetAsync(u => u.Email == request.UserForLoginDto.Email);

        _businessRules.DeveloperShouldExistWhenRequested(developer);

        _businessRules.DeveloperCredentialsShouldMatch(request.UserForLoginDto.Password, developer!.PasswordHash, developer.PasswordSalt);

        AccessToken accessToken = _tokenHelper.CreateToken(developer, new List<OperationClaim>());

        AccessTokenDto accessTokenDto = _mapper.Map<AccessTokenDto>(accessToken);
        return accessTokenDto;
    }
}