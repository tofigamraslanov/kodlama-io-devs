﻿using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;

public record CreateProgrammingLanguageCommand(string Name) : IRequest<CreatedProgrammingLanguageDto>;

public class
    CreateProgrammingLanguageCommandHandler : IRequestHandler<CreateProgrammingLanguageCommand,
        CreatedProgrammingLanguageDto>
{
    private readonly IProgrammingLanguageRepository _repository;
    private readonly IMapper _mapper;
    private readonly ProgrammingLanguageBusinessRules _businessRules;

    public CreateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository repository,
        IMapper mapper,
        ProgrammingLanguageBusinessRules businessRules)
    {
        _repository = repository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<CreatedProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request,
        CancellationToken cancellationToken)
    {
        await _businessRules.ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(request.Name);

        ProgrammingLanguage mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);

        ProgrammingLanguage createdProgrammingLanguage =
            await _repository.AddAsync(mappedProgrammingLanguage);

        CreatedProgrammingLanguageDto createdProgrammingLanguageDto =
            _mapper.Map<CreatedProgrammingLanguageDto>(createdProgrammingLanguage);

        return createdProgrammingLanguageDto;
    }
}