using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;

public record CreateGitHubProfileCommand(int DeveloperId, string GitHubAddress) : IRequest<CreatedGitHubProfileDto>;

public class CreateGitHubProfileCommandHandler : IRequestHandler<CreateGitHubProfileCommand, CreatedGitHubProfileDto>
{
    private readonly IGitHubProfileRepository _repository;
    private readonly IMapper _mapper;
    private readonly GitHubProfileBusinessRules _businessRules;
    
    public CreateGitHubProfileCommandHandler(IGitHubProfileRepository repository, IMapper mapper, GitHubProfileBusinessRules businessRules)
    {
        _repository = repository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<CreatedGitHubProfileDto> Handle(CreateGitHubProfileCommand request, CancellationToken cancellationToken)
    {
        await _businessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.GitHubAddress);
            
        GitHubProfile gitHubProfile = _mapper.Map<GitHubProfile>(request);

        GitHubProfile createdGithubProfile = await _repository.AddAsync(gitHubProfile);

        CreatedGitHubProfileDto createdGitHubProfileDto = _mapper.Map<CreatedGitHubProfileDto>(createdGithubProfile);
        return createdGitHubProfileDto;
    }
}