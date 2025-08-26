using Application.Features.Commands.InternetLinesCommands;
using FluentValidation;

namespace Application.Validations;

public sealed class CreateInternetLinesCommadValidator : AbstractValidator<CreateInternetLinesCommad>
{
	public CreateInternetLinesCommadValidator()
	{
		RuleFor(x => x.LineNumber)
			.NotEmpty().WithMessage("Hat numarası zorunludur.");

		RuleFor(x => x.Provider)
			.NotEmpty().WithMessage("Sağlayıcı zorunludur.");

		RuleFor(x => x.Speed)
			.NotEmpty().WithMessage("Hız bilgisi zorunludur.");

		RuleFor(x => x.ContractEndDate)
			.GreaterThan(DateTime.UtcNow.Date).WithMessage("Sözleşme bitiş tarihi gelecekte olmalıdır.");

		RuleFor(x => x.LocationId)
			.NotEmpty().WithMessage("Lokasyon zorunludur.");
	}
}


