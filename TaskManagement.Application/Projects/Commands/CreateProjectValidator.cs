using FluentValidation;

namespace TaskManagement.Application.Projects.Commands;

public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .MinimumLength(3).WithMessage("The project name should not be less than 3 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Descriptions should not exceed 500 characters.");
    }
}