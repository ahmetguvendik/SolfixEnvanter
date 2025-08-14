using Application.Features.Commands.DomainNameCommands;
using FluentValidation;

namespace Application.Validations;

public sealed class CreateDomainNameCommandValidator : AbstractValidator<CreateDomainNameCommand>
{
	public CreateDomainNameCommandValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Alan adı zorunludur.")
			.Matches(@"^[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("Geçerli bir alan adı giriniz.");

		RuleFor(x => x.Registrar)
			.NotEmpty().WithMessage("Registrar zorunludur.");

		RuleFor(x => x.RegistrationDate)
			.LessThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Kayıt tarihi gelecekte olamaz.");

		RuleFor(x => x.ExpirationDate)
			.GreaterThan(x => x.RegistrationDate).WithMessage("Bitiş tarihi kayıt tarihinden sonra olmalıdır.");

		RuleFor(x => x.ServerIP)
			.Matches(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")
			.When(x => !string.IsNullOrWhiteSpace(x.ServerIP))
			.WithMessage("Geçerli bir IPv4 adresi giriniz.");

		RuleFor(x => x.Notes)
			.MaximumLength(500).WithMessage("Notlar en fazla 500 karakter olabilir.");
	}
}


