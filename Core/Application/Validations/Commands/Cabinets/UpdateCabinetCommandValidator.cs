using Application.Features.Commands;
using FluentValidation;

namespace Application.Validations;

public sealed class UpdateCabinetCommandValidator : AbstractValidator<UpdateCabinetCommand>
{
	public UpdateCabinetCommandValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty().WithMessage("ID zorunludur.");

		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Kabin adı zorunludur.")
			.MaximumLength(100).WithMessage("Kabin adı 100 karakterden uzun olamaz.");

		RuleFor(x => x.Code)
			.MaximumLength(20).When(x => !string.IsNullOrEmpty(x.Code))
			.WithMessage("Kabin kodu 20 karakterden uzun olamaz.");

		RuleFor(x => x.UHeight)
			.InclusiveBetween(1, 50).When(x => x.UHeight.HasValue)
			.WithMessage("U yüksekliği 1-50 arasında olmalıdır.");

		RuleFor(x => x.Manufacturer)
			.MaximumLength(50).When(x => !string.IsNullOrEmpty(x.Manufacturer))
			.WithMessage("Üretici 50 karakterden uzun olamaz.");

		RuleFor(x => x.Model)
			.MaximumLength(50).When(x => !string.IsNullOrEmpty(x.Model))
			.WithMessage("Model 50 karakterden uzun olamaz.");

		RuleFor(x => x.SerialNumber)
			.MaximumLength(50).When(x => !string.IsNullOrEmpty(x.SerialNumber))
			.WithMessage("Seri numarası 50 karakterden uzun olamaz.");

		RuleFor(x => x.PowerFeed)
			.MaximumLength(100).When(x => !string.IsNullOrEmpty(x.PowerFeed))
			.WithMessage("Güç beslemesi 100 karakterden uzun olamaz.");

		RuleFor(x => x.MaxLoadKg)
			.GreaterThan(0).When(x => x.MaxLoadKg.HasValue)
			.WithMessage("Maksimum yük 0'dan büyük olmalıdır.");

		RuleFor(x => x.MaxPowerWatts)
			.GreaterThan(0).When(x => x.MaxPowerWatts.HasValue)
			.WithMessage("Maksimum güç 0'dan büyük olmalıdır.");

		RuleFor(x => x.CoolingType)
			.MaximumLength(50).When(x => !string.IsNullOrEmpty(x.CoolingType))
			.WithMessage("Soğutma tipi 50 karakterden uzun olamaz.");

		RuleFor(x => x.Notes)
			.MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Notes))
			.WithMessage("Notlar 500 karakterden uzun olamaz.");

		RuleFor(x => x.LocationId)
			.NotEmpty().WithMessage("Lokasyon zorunludur.");
	}
}
