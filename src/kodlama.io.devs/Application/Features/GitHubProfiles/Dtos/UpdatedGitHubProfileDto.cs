namespace Application.Features.GitHubProfiles.Dtos;

public class UpdatedGitHubProfileDto
{
    public int Id { get; set; }
    public int DeveloperId { get; set; }
    public string GitHubAddress { get; set; } = null!;
}