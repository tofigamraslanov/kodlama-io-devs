using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;

public class UpdateGitHubProfileCommand : IRequest<UpdatedGitHubProfileDto>, ISecuredRequest
{
    public int Id { get; set; }
    public string ProfileName { get; set; } = null!;

    public string[] Roles { get; } = { "User" };
}

public class UpdateGitHubProfileCommandHandler : IRequestHandler<UpdateGitHubProfileCommand, UpdatedGitHubProfileDto>
{
    private readonly IGitHubProfileRepository _repository;
    private readonly IMapper _mapper;
    private readonly GitHubProfileBusinessRules _businessRules;

    public UpdateGitHubProfileCommandHandler(IGitHubProfileRepository repository, IMapper mapper, GitHubProfileBusinessRules businessRules)
    {
        _repository = repository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<UpdatedGitHubProfileDto> Handle(UpdateGitHubProfileCommand request,
        CancellationToken cancellationToken)
    {
        GitHubProfile gitHubProfile = await _businessRules.GithubProfileShouldExistBeforeDeletedOrUpdated(request.Id);

        _businessRules.UserMustVerifiedBeforeProfileDeletedOrUpdated(gitHubProfile.UserId);

        ReceivedGithubProfileDto receivedGithubProfileDto = await _businessRules.GithubProfileShouldExistBeforeAdded(request.ProfileName);

        GitHubProfile mappedGitHubProfile = _mapper.Map<GitHubProfile>(receivedGithubProfileDto);
        mappedGitHubProfile.Id = gitHubProfile.Id;
        mappedGitHubProfile!.UserId = gitHubProfile.UserId;

        GitHubProfile updatedGitHubProfile = await _repository.UpdateAsync(mappedGitHubProfile);

        UpdatedGitHubProfileDto updatedGitHubProfileDto = _mapper.Map<UpdatedGitHubProfileDto>(updatedGitHubProfile);
        return updatedGitHubProfileDto;
    }
}