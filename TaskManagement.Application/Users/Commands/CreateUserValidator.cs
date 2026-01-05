using FluentValidation;

namespace TaskManagement.Application.Users.Commands;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
	public CreateUserValidator()
	{
		RuleFor(x => x.Username)
			.NotEmpty().WithMessage("Username is required.")
			.MinimumLength(3).WithMessage("Username must be at least 3 characters.");

		RuleFor(x => x.Email)
			.NotEmpty().WithMessage("Email is required.")
			.EmailAddress().WithMessage("The email format entered is not correct.");

		RuleFor(x => x.Password)
			.NotEmpty().WithMessage("Password is required")
			.MinimumLength(6).WithMessage("Password must be at least 6 characters");
	}
}