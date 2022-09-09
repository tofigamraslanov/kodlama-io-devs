using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technologies.Commands.UpdateTechnology;

public record UpdateTechnologyCommand(int Id, string Name, int ProgrammingLanguageId) : IRequest<UpdatedTechnologyDto>;

public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
{
    private readonly ITechnologyRepository _repository;
    private readonly IMapper _mapper;
    private readonly TechnologyBusinessRules _businessRules;

    public UpdateTechnologyCommandHandler(ITechnologyRepository repository, IMapper mapper, TechnologyBusinessRules businessRules)
    {
        _repository = repository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
    {
        Technology? technology = await _repository.GetAsync(t => t.Id == request.Id);

        _businessRules.TechnologyShouldExistWhenRequested(technology);

        technology!.Name = request.Name;
        technology.ProgrammingLanguageId = request.ProgrammingLanguageId;

        Technology updatedTechnology = await _repository.UpdateAsync(technology);

        UpdatedTechnologyDto updatedTechnologyDto = _mapper.Map<UpdatedTechnologyDto>(updatedTechnology);
        return updatedTechnologyDto;
    }
}