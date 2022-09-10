using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommand : UserForRegisterDto, IRequest<AccessTokenDto>
{
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AccessTokenDto>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly UserBusinessRules _businessRules;
    private readonly IMapper _mapper;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public RegisterUserCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper,
        UserBusinessRules businessRules, IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository)
    {
        _userRepository = userRepository;
        _tokenHelper = tokenHelper;
        _businessRules = businessRules;
        _mapper = mapper;
        _userOperationClaimRepository = userOperationClaimRepository;
    }

    public async Task<AccessTokenDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        await _businessRules.UserEmailCanNotBeDuplicatedWhenInserted(request.Email);

        HashingHelper.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

        User user = _mapper.Map<User>(request);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        user.Status = true;

        User createdUser = await _userRepository.AddAsync(user);

        await _userOperationClaimRepository.AddAsync(new()
        {
            UserId = createdUser.Id,
            OperationClaimId = 2
        });

        var userClaims = await _userOperationClaimRepository.GetListAsync(x =>
                x.UserId == createdUser.Id,
            include: x => x.Include(c => c.OperationClaim),
            cancellationToken: cancellationToken);

        AccessToken accessToken =
            _tokenHelper.CreateToken(createdUser, userClaims.Items.Select(uc => uc.OperationClaim).ToList());

        AccessTokenDto accessTokenDto = _mapper.Map<AccessTokenDto>(accessToken);
        return accessTokenDto;
    }
}