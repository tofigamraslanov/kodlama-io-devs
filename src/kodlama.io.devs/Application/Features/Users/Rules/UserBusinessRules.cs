using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;

namespace Application.Features.Users.Rules;

public class UserBusinessRules
{
    private readonly IUserRepository _repository;

    public UserBusinessRules(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task UserEmailCanNotBeDuplicatedWhenInserted(string email)
    {
        IPaginate<User> users = await _repository.GetListAsync(t => t.Email == email);
        if (users.Items.Any())
            throw new BusinessException("User email already exists");
    }

    public void UserShouldExistWhenRequested(User? user)
    {
        if (user is null)
            throw new BusinessException("User does not exist");
    }

    public void UserCredentialsShouldMatch(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        bool result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
        if (!result)
            throw new BusinessException("User credentials do not match");
    }
}