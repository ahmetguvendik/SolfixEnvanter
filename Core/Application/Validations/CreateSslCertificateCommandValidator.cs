using Application.Features.Commands.SslCertificateCommands;
using FluentValidation;

namespace Application.Validations;

public sealed class CreateSslCertificateCommandValidator : AbstractValidator<CreateSslCertificateCommand>
{
	public CreateSslCertificateCommandValidator()
	{
		RuleFor(x => x.CommonName)
			.NotEmpty().WithMessage("Common Name zorunludur.");

		RuleFor(x => x.Provider)
			.NotEmpty().WithMessage("Sağlayıcı zorunludur.");

		RuleFor(x => x.StartDate)
			.LessThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Başlangıç tarihi gelecekte olamaz.");

		RuleFor(x => x.ExpirationDate)
			.GreaterThan(x => x.StartDate).WithMessage("Bitiş tarihi başlangıç tarihinden sonra olmalıdır.");

		RuleFor(x => x.DomainId)
			.NotEmpty().WithMessage("Domain zorunludur.");

		RuleFor(x => x.Notes)
			.MaximumLength(500).WithMessage("Notlar en fazla 500 karakter olabilir.");
	}
}


