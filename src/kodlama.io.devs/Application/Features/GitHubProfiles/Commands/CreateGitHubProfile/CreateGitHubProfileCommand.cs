using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;

public class CreateGitHubProfileCommand : IRequest<CreatedGitHubProfileDto>, ISecuredRequest
{
    public string ProfileName { get; set; } = null!;

    public string[] Roles { get; } = { "Admin", "User" };
}

public class CreateGitHubProfileCommandHandler : IRequestHandler<CreateGitHubProfileCommand, CreatedGitHubProfileDto>
{
    private readonly IGitHubProfileRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly GitHubProfileBusinessRules _businessRules;

    public CreateGitHubProfileCommandHandler(IGitHubProfileRepository repository, IHttpContextAccessor httpContextAccessor, IMapper mapper,
        GitHubProfileBusinessRules businessRules)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<CreatedGitHubProfileDto> Handle(CreateGitHubProfileCommand request, CancellationToken cancellationToken)
    {
        ReceivedGithubProfileDto receivedGithubProfileDto = await _businessRules.GithubProfileShouldExistBeforeAdded(request.ProfileName);

        GitHubProfile gitHubProfile = _mapper.Map<GitHubProfile>(receivedGithubProfileDto);
        gitHubProfile.UserId = _httpContextAccessor.HttpContext.User.GetUserId();
        
        GitHubProfile createdGithubProfile = await _repository.AddAsync(gitHubProfile);

        CreatedGitHubProfileDto createdGitHubProfileDto = _mapper.Map<CreatedGitHubProfileDto>(createdGithubProfile);
        return createdGitHubProfileDto;
    }
}