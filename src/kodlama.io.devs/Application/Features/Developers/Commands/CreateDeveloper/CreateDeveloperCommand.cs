using Application.Features.Developers.Dtos;
using Application.Features.Developers.Rules;
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
    private readonly ITokenHelper _tokenHelper;
    private readonly DeveloperBusinessRules _businessRules;
    private readonly IMapper _mapper;

    public CreateDeveloperCommandHandler(IDeveloperRepository repository, ITokenHelper tokenHelper,
        DeveloperBusinessRules businessRules, IMapper mapper)
    {
        _repository = repository;
        _tokenHelper = tokenHelper;
        _businessRules = businessRules;
        _mapper = mapper;
    }

    public async Task<AccessTokenDto> Handle(CreateDeveloperCommand request, CancellationToken cancellationToken)
    {
        await _businessRules.DeveloperEmailCanNotBeDuplicatedWhenInserted(request.UserForRegisterDto.Email);

        HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out var passwordHash, out var passwordSalt);

        Developer developer = _mapper.Map<Developer>(request);
        developer.PasswordHash = passwordHash;
        developer.PasswordSalt = passwordSalt;

        User createdDeveloper = await _repository.AddAsync(developer);

        AccessToken accessToken = _tokenHelper.CreateToken(developer, new List<OperationClaim>());
        
        AccessTokenDto accessTokenDto = _mapper.Map<AccessTokenDto>(accessToken);
        return accessTokenDto;
    }
}