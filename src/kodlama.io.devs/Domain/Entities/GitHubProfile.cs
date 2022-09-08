using Core.Persistence.Repositories;

namespace Domain.Entities;

public class UserGitHub : Entity
{
    public int UserId { get; set; }
    public string GithubAddress { get; set; } = null!;

    public UserGitHub()
    {
    }

    public UserGitHub(int id, int userId, string githubAddress)
    {
        Id = id;
        UserId = userId;
        GithubAddress = githubAddress;
    }
}