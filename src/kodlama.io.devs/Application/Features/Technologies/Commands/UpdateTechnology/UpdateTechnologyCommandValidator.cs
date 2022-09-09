using FluentValidation;

namespace Application.Features.Technologies.Commands.UpdateTechnology;

public class UpdateTechnologyCommandValidator : AbstractValidator<UpdateTechnologyCommand>
{
    public UpdateTechnologyCommandValidator()
    {
        RuleFor(t => t.Name).NotEmpty();
    }
}