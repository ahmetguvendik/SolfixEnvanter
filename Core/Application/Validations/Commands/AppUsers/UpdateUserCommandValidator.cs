using Application.Features.Commands;
using FluentValidation;

namespace Application.Validations;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
	public UpdateUserCommandValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty().WithMessage("ID zorunludur.");

		RuleFor(x => x.UserName)
			.NotEmpty().WithMessage("Kullanıcı adı zorunludur.")
			.MaximumLength(50).WithMessage("Kullanıcı adı 50 karakterden uzun olamaz.")
			.Matches("^[a-zA-Z0-9_]+$").WithMessage("Kullanıcı adı sadece harf, rakam ve alt çizgi içerebilir.");

		RuleFor(x => x.Email)
			.NotEmpty().WithMessage("E-posta adresi zorunludur.")
			.EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
			.MaximumLength(100).WithMessage("E-posta adresi 100 karakterden uzun olamaz.");

		RuleFor(x => x.FullName)
			.NotEmpty().WithMessage("Ad soyad zorunludur.")
			.MaximumLength(100).WithMessage("Ad soyad 100 karakterden uzun olamaz.");
	}
}
