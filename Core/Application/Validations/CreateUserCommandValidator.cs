using Application.Features.Commands.AppUser;
using FluentValidation;

namespace Application.Validations;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
	public CreateUserCommandValidator()
	{
		RuleFor(x => x.Username)
			.NotEmpty().WithMessage("Kullanıcı adı zorunludur.")
			.MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.")
			.MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olabilir.");

		RuleFor(x => x.NameSurname)
			.NotEmpty().WithMessage("Ad Soyad zorunludur.")
			.MaximumLength(100).WithMessage("Ad Soyad en fazla 100 karakter olabilir.");

		RuleFor(x => x.Password)
			.NotEmpty().WithMessage("Şifre zorunludur.")
			.MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

		RuleFor(x => x.Email)
			.NotEmpty().WithMessage("Email zorunludur.")
			.EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

		RuleFor(x => x.DepartmanId)
			.NotEmpty().WithMessage("Departman zorunludur.");

		RuleFor(x => x.Role)
			.NotEmpty().WithMessage("Rol zorunludur.");
	}
}


