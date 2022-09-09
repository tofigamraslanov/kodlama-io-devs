using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Hashing;
using Domain.Entities;

namespace Application.Features.Developers.Rules;

public class DeveloperBusinessRules
{
    private readonly IDeveloperRepository _repository;

    public DeveloperBusinessRules(IDeveloperRepository repository)
    {
        _repository = repository;
    }

    public async Task DeveloperEmailCanNotBeDuplicatedWhenInserted(string email)
    {
        IPaginate<Developer> developers = await _repository.GetListAsync(t => t.Email == email);
        if (developers.Items.Any())
            throw new BusinessException("Developer email already exists");
    }

    public void DeveloperShouldExistWhenRequested(Developer? developer)
    {
        if (developer is null)
            throw new BusinessException("Requested developer does not exist");
    }

    public void DeveloperCredentialsShouldMatch(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        bool result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
        if (!result)
            throw new BusinessException("Developer credentials do not match");
    }
}