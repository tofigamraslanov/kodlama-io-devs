using Application.Features.Developers.Commands.CreateDeveloper;
using Application.Features.Users.Dtos;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Features.Developers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateDeveloperCommand, User>().ReverseMap();

        CreateMap<AccessToken, AccessTokenDto>().ReverseMap();
    }
}