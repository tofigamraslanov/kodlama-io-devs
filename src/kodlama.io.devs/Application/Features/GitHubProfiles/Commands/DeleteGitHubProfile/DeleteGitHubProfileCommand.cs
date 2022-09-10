using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;

public class DeleteGitHubProfileCommand : IRequest<DeletedGitHubProfileDto>, ISecuredRequest
{
    public int Id { get; set; }
   
    public string[] Roles { get; } = { "User" };
}

public class DeleteGitHubProfileCommandHandler : IRequestHandler<DeleteGitHubProfileCommand, DeletedGitHubProfileDto>
{
    private readonly IGitHubProfileRepository _repository;
    private readonly IMapper _mapper;
    private readonly GitHubProfileBusinessRules _businessRules;

    public DeleteGitHubProfileCommandHandler(IGitHubProfileRepository repository, IMapper mapper, GitHubProfileBusinessRules businessRules)
    {
        _repository = repository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<DeletedGitHubProfileDto> Handle(DeleteGitHubProfileCommand request,
        CancellationToken cancellationToken)
    {
        GitHubProfile gitHubProfile = await _businessRules.GithubProfileShouldExistBeforeDeletedOrUpdated(request.Id);

        _businessRules.UserMustVerifiedBeforeProfileDeletedOrUpdated(gitHubProfile.UserId);

        await _repository.DeleteAsync(gitHubProfile!);

        DeletedGitHubProfileDto deletedGitHubProfileDto = _mapper.Map<DeletedGitHubProfileDto>(gitHubProfile);
        return deletedGitHubProfileDto;
    }
}