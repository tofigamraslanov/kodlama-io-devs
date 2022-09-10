using FluentValidation;

namespace Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;

public class CreateGitHubProfileCommandValidator : AbstractValidator<CreateGitHubProfileCommand>
{
    public CreateGitHubProfileCommandValidator()
    {
        RuleFor(g => g.ProfileName).NotEmpty().WithMessage("You must enter a profile name");
    }
}