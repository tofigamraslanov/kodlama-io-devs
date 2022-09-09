using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubProfiles.Queries.GetByIdGitHubProfile;

public record GetByIdGitHubProfileQuery(int Id) : IRequest<GitHubProfileGetByIdDto>;

public class GetByIdGitHubProfileQueryHandler : IRequestHandler<GetByIdGitHubProfileQuery, GitHubProfileGetByIdDto>
{
    private readonly IGitHubProfileRepository _repository;
    private readonly IMapper _mapper;
    private readonly GitHubProfileBusinessRules _businessRules;

    public GetByIdGitHubProfileQueryHandler(IGitHubProfileRepository repository, IMapper mapper, GitHubProfileBusinessRules businessRules)
    {
        _repository = repository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<GitHubProfileGetByIdDto> Handle(GetByIdGitHubProfileQuery request, CancellationToken cancellationToken)
    {
        GitHubProfile? gitHubProfile = await _repository.GetAsync(g => g.Id == request.Id);

        _businessRules.GitHubProfileShouldExistWhenRequested(gitHubProfile);

        GitHubProfileGetByIdDto gitHubProfileGetByIdDto = _mapper.Map<GitHubProfileGetByIdDto>(gitHubProfile);
        return gitHubProfileGetByIdDto;
    }
}