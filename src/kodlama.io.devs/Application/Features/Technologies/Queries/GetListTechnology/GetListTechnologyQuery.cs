using Application.Features.Technologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Technologies.Queries.GetListTechnology;

public record GetListTechnologyQuery(PageRequest PageRequest) : IRequest<TechnologyListModel>;

public class GetListTechnologyQueryHandler : IRequestHandler<GetListTechnologyQuery, TechnologyListModel>
{
    private readonly ITechnologyRepository _repository;
    private readonly IMapper _mapper;

    public GetListTechnologyQueryHandler(ITechnologyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TechnologyListModel> Handle(GetListTechnologyQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Technology> technologies = await _repository.GetListAsync(
            include: t => t.Include(t => t.ProgrammingLanguage),
            index: request.PageRequest.Page,
            size: request.PageRequest.PageSize,
            cancellationToken: cancellationToken);

        TechnologyListModel mappedTechnologies = _mapper.Map<TechnologyListModel>(technologies);

        return mappedTechnologies;
    }
}