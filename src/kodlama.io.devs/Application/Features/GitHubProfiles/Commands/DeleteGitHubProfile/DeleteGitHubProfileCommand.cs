using Application.Features.GitHubProfiles.Dtos;
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

    public DeleteGitHubProfileCommandHandler(IGitHubProfileRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DeletedGitHubProfileDto> Handle(DeleteGitHubProfileCommand request,
        CancellationToken cancellationToken)
    {
        GitHubProfile? gitHubProfile = await _repository.GetAsync(p => p.Id == request.Id);

        GitHubProfile deletedGitHubProfile = await _repository.DeleteAsync(gitHubProfile!);

        DeletedGitHubProfileDto deletedGitHubProfileDto = _mapper.Map<DeletedGitHubProfileDto>(deletedGitHubProfile);
        return deletedGitHubProfileDto;
    }
}