using Application.Features.Commands.CabinetCommands;
using FluentValidation;

namespace Application.Validations;

public sealed class CreateCabinetCommandValidator : AbstractValidator<CreateCabinetCommand>
{
	public CreateCabinetCommandValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Dolap adı zorunludur.")
			.MaximumLength(100).WithMessage("Dolap adı en fazla 100 karakter olabilir.");

		RuleFor(x => x.LocationId)
			.NotEmpty().WithMessage("Lokasyon zorunludur.");

		RuleFor(x => x.Code)
			.MaximumLength(50).WithMessage("Kod en fazla 50 karakter olabilir.");

		RuleFor(x => x.UHeight)
			.GreaterThan(0).When(x => x.UHeight.HasValue).WithMessage("U yüksekliği pozitif olmalıdır.");

		RuleFor(x => x.MaxLoadKg)
			.GreaterThan(0).When(x => x.MaxLoadKg.HasValue).WithMessage("Maksimum yük pozitif olmalıdır.");

		RuleFor(x => x.MaxPowerWatts)
			.GreaterThan(0).When(x => x.MaxPowerWatts.HasValue).WithMessage("Maksimum güç pozitif olmalıdır.");

		RuleFor(x => x.Notes)
			.MaximumLength(500).WithMessage("Notlar en fazla 500 karakter olabilir.");
	}
}


