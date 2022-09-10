using Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;
using Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
using Core.Security.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class GitHubProfilesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGitHubProfileCommand command)
    {
        CreatedGitHubProfileDto result = await Mediator!.Send(command);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateGitHubProfileCommand command)
    {
        UpdatedGitHubProfileDto result = await Mediator!.Send(command);
        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteGitHubProfileCommand command)
    {
        DeletedGitHubProfileDto result = await Mediator!.Send(command);
        return Ok(result);
    }
}