using Application.Features.Technologies.Dtos;
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

    public UpdateTechnologyCommandHandler(ITechnologyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
    {
        Technology? technology = await _repository.GetAsync(t => t.Id == request.Id);

        technology!.Name = request.Name;
        technology.ProgrammingLanguageId = request.ProgrammingLanguageId;

        Technology updatedTechnology = await _repository.UpdateAsync(technology);

        UpdatedTechnologyDto mappedUpdatedTechnologyDto = _mapper.Map<UpdatedTechnologyDto>(updatedTechnology);

        return mappedUpdatedTechnologyDto;
    }
}