namespace Application.Features.GitHubProfiles.Dtos;

public class GitHubProfileGetByIdDto
{
    public int Id { get; set; }
    public int DeveloperId { get; set; }
    public string GitHubAddress { get; set; } = null!;
}