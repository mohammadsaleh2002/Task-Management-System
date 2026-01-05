using FluentValidation;

namespace TaskManagement.Application.Tasks.Commands;

public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Task title is required.");
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("Project ID is required.");
        RuleFor(x => x.AssignedUserId).NotEmpty().WithMessage("The task should be delegated to one person.");
    }
}