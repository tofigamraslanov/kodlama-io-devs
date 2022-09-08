using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Technologies.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Technology, TechnologyListDto>().ForMember(m => m.ProgrammingLanguageName,
            option => option.MapFrom(t => t.ProgrammingLanguage.Name)).ReverseMap();
        CreateMap<IPaginate<Technology>, TechnologyListModel>().ReverseMap();

        CreateMap<CreateTechnologyCommand, Technology>().ReverseMap();
        CreateMap<Technology, CreatedTechnologyDto>().ReverseMap();

        CreateMap<Technology, UpdatedTechnologyDto>().ReverseMap();

        CreateMap<Technology, DeletedTechnologyDto>().ReverseMap();

        CreateMap<Technology, TechnologyGetByIdDto>().ReverseMap();
    }
}