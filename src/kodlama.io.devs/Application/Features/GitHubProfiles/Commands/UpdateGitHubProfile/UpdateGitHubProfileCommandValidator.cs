using FluentValidation;

namespace Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;

public class UpdateGitHubProfileCommandValidator : AbstractValidator<UpdateGitHubProfileCommand>
{
    public UpdateGitHubProfileCommandValidator()
    {
        RuleFor(g => g.GitHubAddress).NotEmpty();
    }
}