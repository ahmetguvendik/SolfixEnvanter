using Application.Features.Commands;
using FluentValidation;

namespace Application.Validations;

public sealed class UpdateDomainNameCommandValidator : AbstractValidator<UpdateDomainNameCommand>
{
	public UpdateDomainNameCommandValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty().WithMessage("ID zorunludur.");

		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Domain adı zorunludur.")
			.MaximumLength(100).WithMessage("Domain adı 100 karakterden uzun olamaz.")
			.Matches(@"^[a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?(\.[a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?)*$")
			.WithMessage("Geçerli bir domain adı giriniz.");

		RuleFor(x => x.Registrar)
			.NotEmpty().WithMessage("Domain kayıt şirketi zorunludur.")
			.MaximumLength(100).WithMessage("Domain kayıt şirketi 100 karakterden uzun olamaz.");

		RuleFor(x => x.RegistrationDate)
			.NotEmpty().WithMessage("Kayıt tarihi zorunludur.")
			.LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Kayıt tarihi gelecekte olamaz.");

		RuleFor(x => x.ExpirationDate)
			.NotEmpty().WithMessage("Bitiş tarihi zorunludur.")
			.GreaterThan(DateTime.UtcNow).WithMessage("Bitiş tarihi gelecekte olmalıdır.")
			.GreaterThan(x => x.RegistrationDate).WithMessage("Bitiş tarihi kayıt tarihinden sonra olmalıdır.");

		RuleFor(x => x.HostingProvider)
			.MaximumLength(100).When(x => !string.IsNullOrEmpty(x.HostingProvider))
			.WithMessage("Hosting sağlayıcısı 100 karakterden uzun olamaz.");

		RuleFor(x => x.HostingPlan)
			.MaximumLength(100).When(x => !string.IsNullOrEmpty(x.HostingPlan))
			.WithMessage("Hosting planı 100 karakterden uzun olamaz.");

		RuleFor(x => x.ServerIP)
			.Matches(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")
			.When(x => !string.IsNullOrEmpty(x.ServerIP))
			.WithMessage("Geçerli bir IP adresi giriniz.");

		RuleFor(x => x.HostingExpirationDate)
			.GreaterThan(DateTime.UtcNow).When(x => x.HostingExpirationDate.HasValue)
			.WithMessage("Hosting bitiş tarihi gelecekte olmalıdır.");

		RuleFor(x => x.Notes)
			.MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Notes))
			.WithMessage("Notlar 500 karakterden uzun olamaz.");
	}
}
