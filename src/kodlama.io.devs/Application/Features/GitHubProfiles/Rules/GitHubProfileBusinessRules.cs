using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.GitHubProfiles.Rules;

public class GitHubProfileBusinessRules
{
    private readonly IGitHubProfileRepository _repository;

    public GitHubProfileBusinessRules(IGitHubProfileRepository repository)
    {
        _repository = repository;
    }

    public async Task TechnologyNameCanNotBeDuplicatedWhenInserted(string gitHubAddress)
    {
        IPaginate<GitHubProfile> gitHubProfiles = await _repository.GetListAsync(t => t.GitHubAddress == gitHubAddress);
        if (gitHubProfiles.Items.Any())
            throw new BusinessException("GitHub address already exists");
    }

    public void GitHubProfileShouldExistWhenRequested(GitHubProfile? gitHubProfile)
    {
        if (gitHubProfile is null)
            throw new BusinessException("Requested GitHub profile does not exist");
    }
}