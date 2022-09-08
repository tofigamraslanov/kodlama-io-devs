using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage;

public record GetByIdProgrammingLanguageQuery(int Id) : IRequest<ProgrammingLanguageGetByIdDto>;

public class
    GetByIdProgrammingLanguageQueryHandler : IRequestHandler<GetByIdProgrammingLanguageQuery,
        ProgrammingLanguageGetByIdDto>
{
    private readonly IProgrammingLanguageRepository _repository;
    private readonly ProgrammingLanguageBusinessRules _businessRules;
    private readonly IMapper _mapper;

    public GetByIdProgrammingLanguageQueryHandler(IProgrammingLanguageRepository repository,
        ProgrammingLanguageBusinessRules businessRules, IMapper mapper)
    {
        _repository = repository;
        _businessRules = businessRules;
        _mapper = mapper;
    }

    public async Task<ProgrammingLanguageGetByIdDto> Handle(GetByIdProgrammingLanguageQuery request,
        CancellationToken cancellationToken)
    {
        ProgrammingLanguage? programmingLanguage = await _repository.GetAsync(l => l.Id == request.Id);

        _businessRules.ProgrammingLanguageShouldBeExistWhenRequested(programmingLanguage);

        ProgrammingLanguageGetByIdDto programmingLanguageGetByIdDto =
            _mapper.Map<ProgrammingLanguageGetByIdDto>(programmingLanguage);

        return programmingLanguageGetByIdDto;
    }
}