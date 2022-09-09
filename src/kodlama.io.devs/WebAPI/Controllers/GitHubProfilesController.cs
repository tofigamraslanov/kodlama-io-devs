using System.Runtime.CompilerServices;
using Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;
using Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Models;
using Application.Features.GitHubProfiles.Queries.GetByIdGitHubProfile;
using Application.Features.GitHubProfiles.Queries.GetListGitHubProfile;
using Core.Application.Requests;
using Microsoft.AspNetCore.Identity;
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

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListGitHubProfileQuery query = new(pageRequest);
        GitHubProfileListModel result = await Mediator!.Send(query);
        return Ok(result);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdGitHubProfileQuery query)
    {
        GitHubProfileGetByIdDto result = await Mediator!.Send(query);
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