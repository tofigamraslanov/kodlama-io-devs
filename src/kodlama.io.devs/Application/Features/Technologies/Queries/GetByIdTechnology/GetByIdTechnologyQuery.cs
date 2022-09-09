using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technologies.Queries.GetByIdTechnology;

public record GetByIdTechnologyQuery(int Id) : IRequest<TechnologyGetByIdDto>;

public class GetByIdTechnologyQueryHandler : IRequestHandler<GetByIdTechnologyQuery, TechnologyGetByIdDto>
{
    private readonly ITechnologyRepository _repository;
    private readonly IMapper _mapper;
    private readonly TechnologyBusinessRules _businessRules;

    public GetByIdTechnologyQueryHandler(ITechnologyRepository repository, IMapper mapper, TechnologyBusinessRules businessRules)
    {
        _repository = repository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<TechnologyGetByIdDto> Handle(GetByIdTechnologyQuery request, CancellationToken cancellationToken)
    {
        Technology? technology = await _repository.GetAsync(t => t.Id == request.Id);

        _businessRules.TechnologyShouldExistWhenRequested(technology);

        TechnologyGetByIdDto technologyGetByIdDto = _mapper.Map<TechnologyGetByIdDto>(technology);
        return technologyGetByIdDto;
    }
}