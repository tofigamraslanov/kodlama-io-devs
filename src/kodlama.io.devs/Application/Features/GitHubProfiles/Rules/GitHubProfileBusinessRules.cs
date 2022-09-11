using System.Net.Http.Json;
using Application.Features.GitHubProfiles.Dtos;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Extensions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GitHubProfiles.Rules;

public class GitHubProfileBusinessRules
{
    private readonly IGitHubProfileRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;

    public GitHubProfileBusinessRules(IGitHubProfileRepository repository, IHttpContextAccessor httpContextAccessor,
        IHttpClientFactory httpClientFactory)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ReceivedGithubProfileDto> GithubProfileShouldExistBeforeAdded(string profileName)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient("GitHubUserProfile");

        ReceivedGithubProfileDto? receivedGithubProfileDto =
            await httpClient.GetFromJsonAsync<ReceivedGithubProfileDto>(
                requestUri: $"{httpClient.BaseAddress}{profileName}");

        if (receivedGithubProfileDto is null)
            throw new BusinessException($"There is no user with {profileName} profile name");

        return receivedGithubProfileDto;
    }

    public async Task<GitHubProfile> GithubProfileShouldExistBeforeDeletedOrUpdated(int id)
    {
        GitHubProfile? gitHubProfile = await _repository.Query().AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);

        if (gitHubProfile is null)
            throw new BusinessException("GitHub profile does not exist");

        return gitHubProfile;
    }

    public void UserMustVerifiedBeforeProfileDeletedOrUpdated(int id)
    {
        var idFromToken = _httpContextAccessor.HttpContext.User.GetUserId();
        if (id != idFromToken)
        {
            throw new AuthorizationException("You are not authorized");
        }
    }
}