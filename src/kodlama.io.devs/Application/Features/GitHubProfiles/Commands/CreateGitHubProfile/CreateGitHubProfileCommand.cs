using Application.Features.GitHubProfiles.Dtos;
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

    public CreateGitHubProfileCommandHandler(IGitHubProfileRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CreatedGitHubProfileDto> Handle(CreateGitHubProfileCommand request, CancellationToken cancellationToken)
    {
        GitHubProfile gitHubProfile = _mapper.Map<GitHubProfile>(request);

        GitHubProfile createdGithubProfile = await _repository.AddAsync(gitHubProfile);

        CreatedGitHubProfileDto createdGitHubProfileDto = _mapper.Map<CreatedGitHubProfileDto>(createdGithubProfile);
        return createdGitHubProfileDto;
    }
}