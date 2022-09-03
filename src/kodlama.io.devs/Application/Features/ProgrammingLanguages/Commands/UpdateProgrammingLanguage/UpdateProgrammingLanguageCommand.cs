using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;

public class UpdateProgrammingLanguageCommand : IRequest<UpdatedProgrammingLanguageDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public class UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand,
        UpdatedProgrammingLanguageDto>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _businessRules;

        public UpdateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository repository, IMapper mapper,
            ProgrammingLanguageBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<UpdatedProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request,
            CancellationToken cancellationToken)
        {
            ProgrammingLanguage? programmingLanguage = await _repository.GetAsync(l => l.Id == request.Id);

            _businessRules.ProgrammingLanguageShouldBeExistWhenRequested(programmingLanguage);

            programmingLanguage!.Name = request.Name;

            await _businessRules.ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(programmingLanguage.Name);

            ProgrammingLanguage updatedProgrammingLanguage = await _repository.UpdateAsync(programmingLanguage);

            UpdatedProgrammingLanguageDto updatedDto =
                _mapper.Map<UpdatedProgrammingLanguageDto>(updatedProgrammingLanguage);

            return updatedDto;
        }
    }
}