using Application.Features.GitHubProfiles.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubProfiles.Queries.GetListGitHubProfile;

public record GetListGitHubProfileQuery(PageRequest PageRequest) : IRequest<GitHubProfileListModel>;

public class GetListGitHubProfileQueryHandler : IRequestHandler<GetListGitHubProfileQuery, GitHubProfileListModel>
{
    private readonly IGitHubProfileRepository _repository;
    private readonly IMapper _mapper;

    public GetListGitHubProfileQueryHandler(IGitHubProfileRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GitHubProfileListModel> Handle(GetListGitHubProfileQuery request,
        CancellationToken cancellationToken)
    {
        IPaginate<GitHubProfile> gitHubProfiles =
            await _repository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken);

        GitHubProfileListModel mappedGitHubProfiles = _mapper.Map<GitHubProfileListModel>(gitHubProfiles);
        return mappedGitHubProfiles;
    }
}