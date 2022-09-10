using System.Text.Json.Serialization;

namespace Application.Features.GitHubProfiles.Dtos;

public class ReceivedGithubProfileDto
{
    public string Login { get; set; }

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; }

    public string Name { get; set; }
    public string Company { get; set; }
    public string Blog { get; set; }
    public string Location { get; set; }


    [JsonPropertyName("public_repos")]
    public ushort PublicRepos { get; set; }

    public ushort Followers { get; set; }
    public ushort Following { get; set; }
}