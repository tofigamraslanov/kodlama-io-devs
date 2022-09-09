using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;

public record DeleteGitHubProfileCommand
    (int Id) : IRequest<DeletedGitHubProfileDto>;

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
        GitHubProfile? gitHubProfile = await _repository.GetAsync(p => p.Id == request.Id);

        _businessRules.GitHubProfileShouldExistWhenRequested(gitHubProfile);
        
        GitHubProfile deletedGitHubProfile = await _repository.DeleteAsync(gitHubProfile!);

        DeletedGitHubProfileDto deletedGitHubProfileDto = _mapper.Map<DeletedGitHubProfileDto>(deletedGitHubProfile);
        return deletedGitHubProfileDto;
    }
}