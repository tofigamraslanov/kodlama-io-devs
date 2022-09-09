using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technologies.Commands.DeleteTechnology;

public record DeleteTechnologyCommand(int Id) : IRequest<DeletedTechnologyDto>;

public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeletedTechnologyDto>
{
    private readonly ITechnologyRepository _repository;
    private readonly IMapper _mapper;
    private readonly TechnologyBusinessRules _businessRules;

    public DeleteTechnologyCommandHandler(ITechnologyRepository repository, IMapper mapper, TechnologyBusinessRules businessRules)
    {
        _repository = repository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<DeletedTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
    {
        Technology? technology = await _repository.GetAsync(t => t.Id == request.Id);

        _businessRules.TechnologyShouldExistWhenRequested(technology);

        Technology deletedTechnology = await _repository.DeleteAsync(technology!);

        DeletedTechnologyDto deletedTechnologyDto = _mapper.Map<DeletedTechnologyDto>(deletedTechnology);
        return deletedTechnologyDto;
    }
}