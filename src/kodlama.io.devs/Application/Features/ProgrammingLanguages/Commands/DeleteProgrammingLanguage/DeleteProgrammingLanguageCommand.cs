using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;

public record DeleteProgrammingLanguageCommand(int Id) : IRequest<DeletedProgrammingLanguageDto>;

public class
    DeleteProgrammingLanguageCommandHandler : IRequestHandler<DeleteProgrammingLanguageCommand,
        DeletedProgrammingLanguageDto>
{
    private readonly IProgrammingLanguageRepository _repository;
    private readonly IMapper _mapper;
    private readonly ProgrammingLanguageBusinessRules _businessRules;

    public DeleteProgrammingLanguageCommandHandler(IProgrammingLanguageRepository repository, IMapper mapper,
        ProgrammingLanguageBusinessRules businessRules)
    {
        _repository = repository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request,
        CancellationToken cancellationToken)
    {
        ProgrammingLanguage? programmingLanguage = await _repository.GetAsync(l => l.Id == request.Id);

        _businessRules.ProgrammingLanguageShouldBeExistWhenRequested(programmingLanguage);

        ProgrammingLanguage deletedProgrammingLanguage = await _repository.DeleteAsync(programmingLanguage!);

        DeletedProgrammingLanguageDto deletedProgrammingLanguageDto =
            _mapper.Map<DeletedProgrammingLanguageDto>(deletedProgrammingLanguage);

        return deletedProgrammingLanguageDto;
    }
}