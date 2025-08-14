using Application.Features.Commands.AppUser;
using FluentValidation;

namespace Application.Validations;

public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
	public LoginUserCommandValidator()
	{
		RuleFor(x => x.Username)
			.NotEmpty().WithMessage("Kullanıcı adı zorunludur.");

		RuleFor(x => x.Password)
			.NotEmpty().WithMessage("Şifre zorunludur.");
	}
}


