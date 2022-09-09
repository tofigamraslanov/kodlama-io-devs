using Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.GitHubProfiles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateGitHubProfileCommand, GitHubProfile>().ReverseMap();
        CreateMap<GitHubProfile, CreatedGitHubProfileDto>().ReverseMap();

        CreateMap<GitHubProfile, UpdatedGitHubProfileDto>().ReverseMap();

        CreateMap<GitHubProfile, DeletedGitHubProfileDto>().ReverseMap();

        CreateMap<IPaginate<GitHubProfile>, GitHubProfileListModel>().ReverseMap();
        CreateMap<GitHubProfile, GitHubProfileListDto>().ReverseMap();

        CreateMap<GitHubProfile, GitHubProfileGetByIdDto>().ReverseMap();
    }
}