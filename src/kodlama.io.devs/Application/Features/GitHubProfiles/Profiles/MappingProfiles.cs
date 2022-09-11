using Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.GitHubProfiles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ReceivedGithubProfileDto, GitHubProfile>().ReverseMap();

        CreateMap<GitHubProfile, CreatedGitHubProfileDto>().ReverseMap();

        CreateMap<GitHubProfile, UpdatedGitHubProfileDto>().ReverseMap();

        CreateMap<GitHubProfile, DeletedGitHubProfileDto>().ReverseMap();
    }
}