using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Technologies.Rules;

public class TechnologyBusinessRules
{
    private readonly ITechnologyRepository _repository;

    public TechnologyBusinessRules(ITechnologyRepository repository)
    {
        _repository = repository;
    }

    public async Task TechnologyNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<Technology> technologies = await _repository.GetListAsync(t => t.Name == name);
        if (technologies.Items.Any())
            throw new BusinessException("Technology name already exists");
    }

    public void TechnologyShouldExistWhenRequested(Technology? technology)
    {
        if (technology is null)
            throw new BusinessException("Requested technology does not exist");
    }
}