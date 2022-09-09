using Application.Features.Developers.Commands.CreateDeveloper;
using Application.Features.Developers.Dtos;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Entities;

namespace Application.Features.Developers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateDeveloperCommand, Developer>()
            .ForMember(d => d.Email, o => o.MapFrom(s => s.UserForRegisterDto.Email))
            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.UserForRegisterDto.FirstName))
            .ForMember(d => d.LastName, o => o.MapFrom(s => s.UserForRegisterDto.LastName))
            .ReverseMap();

        // CreateMap<AccessToken, AccessTokenDto>()
        //     .ForMember(d => d.AccessToken.Token, o => o.MapFrom(s => s.Token))
        //     .ForMember(d => d.AccessToken.Expiration, o => o.MapFrom(s => s.Expiration))
        //     .ReverseMap();

        CreateMap<AccessTokenDto, AccessToken>()
            .ForMember(d => d.Token, o => o.MapFrom(s => s.AccessToken.Token))
            .ForMember(d => d.Expiration, o => o.MapFrom(s => s.AccessToken.Expiration))
            .ReverseMap();
    }
}