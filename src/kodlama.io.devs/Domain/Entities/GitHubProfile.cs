using Core.Persistence.Repositories;

namespace Domain.Entities;

public class GitHubProfile : Entity
{
    public int DeveloperId { get; set; }
    public string GitHubAddress { get; set; } = null!;

    public virtual Developer Developer { get; set; }

    public GitHubProfile()
    {
    }

    public GitHubProfile(int id, int developerId, string gitHubAddress) : this()
    {
        Id = id;
        DeveloperId = developerId;
        GitHubAddress = gitHubAddress;
    }
}