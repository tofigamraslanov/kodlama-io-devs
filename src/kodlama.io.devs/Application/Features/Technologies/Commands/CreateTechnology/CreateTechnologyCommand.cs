using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technologies.Commands.CreateTechnology;

public record CreateTechnologyCommand
    (string Name, int ProgrammingLanguageId) : IRequest<CreatedTechnologyDto>;

public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
{
    private readonly ITechnologyRepository _repository;
    private readonly IMapper _mapper;
    private readonly TechnologyBusinessRules _businessRules;

    public CreateTechnologyCommandHandler(ITechnologyRepository repository, IMapper mapper, TechnologyBusinessRules businessRules)
    {
        _repository = repository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
    {
        await _businessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.Name);

        Technology mappedTechnology = _mapper.Map<Technology>(request);

        Technology createdTechnology = await _repository.AddAsync(mappedTechnology);

        CreatedTechnologyDto createdTechnologyDto = _mapper.Map<CreatedTechnologyDto>(createdTechnology);

        return createdTechnologyDto;
    }
}