namespace Application.Features.GitHubProfiles.Dtos;

public class CreatedGitHubProfileDto
{
    public string? HtmlUrl { get; set; }
    public string Name { get; set; } = null!;
    public ushort PublicRepos { get; set; }
    public ushort PublicGists { get; set; }
    public ushort Followers { get; set; }
    public ushort Following { get; set; }
}