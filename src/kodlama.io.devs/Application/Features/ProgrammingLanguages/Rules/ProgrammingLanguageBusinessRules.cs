﻿using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.ProgrammingLanguages.Rules;

public class ProgrammingLanguageBusinessRules
{
    private readonly IProgrammingLanguageRepository _repository;

    public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository repository)
    {
        _repository = repository;
    }

    public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<ProgrammingLanguage> result = await _repository.GetListAsync(l => l.Name == name);
        if (result.Items.Any())
            throw new BusinessException("Programming language name exists");
    }

    public void ProgrammingLanguageShouldBeExistWhenRequested(ProgrammingLanguage? programmingLanguage)
    {
        if (programmingLanguage is null)
            throw new BusinessException("Requested programming language does not exists");
    }
}