namespace Application.Features.GitHubProfiles.Dtos;

public class GitHubProfileListDto
{
    public int Id { get; set; }
    public int DeveloperId { get; set; }
    public string GitHubAddress { get; set; } = null!;
}