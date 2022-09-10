using Application.Features.Users.Commands.RegisterUser;
using Application.Features.Users.Dtos;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RegisterUserCommand, User>().ReverseMap();

        CreateMap<AccessTokenDto, AccessToken>().ReverseMap();
    }
}