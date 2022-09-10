using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Commands.LoginUser;

public class LoginUserCommand : UserForLoginDto, IRequest<AccessTokenDto>
{
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AccessTokenDto>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly UserBusinessRules _businessRules;
    private readonly IMapper _mapper;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public LoginUserCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper,
        UserBusinessRules businessRules,
        IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository)
    {
        _userRepository = userRepository;
        _tokenHelper = tokenHelper;
        _businessRules = businessRules;
        _mapper = mapper;
        _userOperationClaimRepository = userOperationClaimRepository;
    }

    public async Task<AccessTokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        //User? user = await _repository.Query().Include(u => u.UserOperationClaims).ThenInclude(u => u.OperationClaim)
        //    .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken: cancellationToken);

        var user = await _userRepository.GetAsync(x => x.Email == request.Email);

        _businessRules.UserShouldExistWhenRequested(user);

        _businessRules.UserCredentialsShouldMatch(request.Password, user!.PasswordHash, user.PasswordSalt);

        var userClaims = await _userOperationClaimRepository.GetListAsync(x =>
                x.UserId == user.Id,
            include: x => x.Include(c => c.OperationClaim),
            cancellationToken: cancellationToken);

        //var operationClaims = user.UserOperationClaims.Select(d => d.OperationClaim);
        //var claims = operationClaims.ToList();

        AccessToken accessToken =
            _tokenHelper.CreateToken(user, userClaims.Items.Select(x => x.OperationClaim).ToList());

        AccessTokenDto accessTokenDto = _mapper.Map<AccessTokenDto>(accessToken);
        return accessTokenDto;
    }
}