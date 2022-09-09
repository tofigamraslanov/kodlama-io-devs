﻿using System.Data;
using FluentValidation;

namespace Application.Features.Technologies.Commands.CreateTechnology;

public class CreateTechnologyCommandValidator : AbstractValidator<CreateTechnologyCommand>
{
    public CreateTechnologyCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
    }
}