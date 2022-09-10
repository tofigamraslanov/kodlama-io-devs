using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities;

public class GitHubProfile : Entity
{
    public int UserId { get; set; }
    public string Login { get; set; } = null!;
    public string HtmlUrl { get; set; } = null!;
    public string? Name { get; set; }
    public string? Company { get; set; }
    public string? Blog { get; set; }
    public string? Location { get; set; }
    public ushort PublicRepos { get; set; }
    public ushort Followers { get; set; }
    public ushort Following { get; set; }

    public virtual User User { get; set; } = null!;

    public GitHubProfile()
    {
    }

    public GitHubProfile(int id, int userId, string htmlUrl, string name, string company, string blog, string location,
        ushort publicRepos,
        ushort followers, ushort following) : this()
    {
        Id = id;
        UserId = userId;
        HtmlUrl = htmlUrl;
        Name = name;
        Company = company;
        Blog = blog;
        Location = location;
        PublicRepos = publicRepos;
        Followers = followers;
        Following = following;
    }
}