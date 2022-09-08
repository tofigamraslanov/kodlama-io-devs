using Application.Features.GitHubProfiles.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;

public record UpdateGitHubProfileCommand
    (int Id, int DeveloperId, string GitHubAddress) : IRequest<UpdatedGitHubProfileDto>;

public class UpdateGitHubProfileCommandHandler : IRequestHandler<UpdateGitHubProfileCommand, UpdatedGitHubProfileDto>
{
    private readonly IGitHubProfileRepository _repository;
    private readonly IMapper _mapper;

    public UpdateGitHubProfileCommandHandler(IGitHubProfileRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UpdatedGitHubProfileDto> Handle(UpdateGitHubProfileCommand request,
        CancellationToken cancellationToken)
    {
        GitHubProfile? gitHubProfile = await _repository.GetAsync(p => p.Id == request.Id);

        gitHubProfile!.DeveloperId = request.DeveloperId;
        gitHubProfile.GitHubAddress = request.GitHubAddress;

        GitHubProfile updatedGitHubProfile = await _repository.UpdateAsync(gitHubProfile);

        UpdatedGitHubProfileDto updatedGitHubProfileDto = _mapper.Map<UpdatedGitHubProfileDto>(updatedGitHubProfile);
        return updatedGitHubProfileDto;
    }
}