using Application.Features.GitHubProfiles.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.GitHubProfiles.Models;

public class GitHubProfileListModel : BasePageableModel
{
    public IList<GitHubProfileListDto> Items { get; set; } = null!;
}