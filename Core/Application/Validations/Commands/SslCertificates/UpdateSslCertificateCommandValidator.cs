using Application.Features.Commands;
using FluentValidation;

namespace Application.Validations;

public sealed class UpdateSslCertificateCommandValidator : AbstractValidator<UpdateSslCertificateCommand>
{
	public UpdateSslCertificateCommandValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty().WithMessage("ID zorunludur.");

		RuleFor(x => x.CommonName)
			.NotEmpty().WithMessage("Ortak ad (Common Name) zorunludur.")
			.MaximumLength(100).WithMessage("Ortak ad 100 karakterden uzun olamaz.")
			.Matches(@"^[a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?(\.[a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?)*$")
			.WithMessage("Geçerli bir domain adı giriniz.");

		RuleFor(x => x.Provider)
			.NotEmpty().WithMessage("SSL sağlayıcısı zorunludur.")
			.MaximumLength(100).WithMessage("SSL sağlayıcısı 100 karakterden uzun olamaz.");

		RuleFor(x => x.StartDate)
			.NotEmpty().WithMessage("Başlangıç tarihi zorunludur.")
			.LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Başlangıç tarihi gelecekte olamaz.");

		RuleFor(x => x.ExpirationDate)
			.NotEmpty().WithMessage("Bitiş tarihi zorunludur.")
			.GreaterThan(DateTime.UtcNow).WithMessage("Bitiş tarihi gelecekte olmalıdır.")
			.GreaterThan(x => x.StartDate).WithMessage("Bitiş tarihi başlangıç tarihinden sonra olmalıdır.");

		RuleFor(x => x.DomainId)
			.NotEmpty().WithMessage("Domain ID zorunludur.");

		RuleFor(x => x.Notes)
			.MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Notes))
			.WithMessage("Notlar 500 karakterden uzun olamaz.");
	}
}
