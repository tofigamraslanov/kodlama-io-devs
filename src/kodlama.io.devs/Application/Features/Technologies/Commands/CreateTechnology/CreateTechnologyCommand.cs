using Application.Features.Technologies.Dtos;
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

    public CreateTechnologyCommandHandler(ITechnologyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
    {
        Technology mappedTechnology = _mapper.Map<Technology>(request);

        Technology createdTechnology = await _repository.AddAsync(mappedTechnology);

        CreatedTechnologyDto createdTechnologyDto = _mapper.Map<CreatedTechnologyDto>(createdTechnology);

        return createdTechnologyDto;
    }
}