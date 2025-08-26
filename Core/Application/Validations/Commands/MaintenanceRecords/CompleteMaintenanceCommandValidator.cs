using Application.Features.Commands.MaintenanceRecordCommands;
using FluentValidation;

namespace Application.Validations;

public sealed class CompleteMaintenanceCommandValidator : AbstractValidator<CompleteMaintenanceCommand>
{
	public CompleteMaintenanceCommandValidator()
	{
		RuleFor(x => x.maintenanceTypeId)
			.NotEmpty().WithMessage("Bakım tipi zorunludur.");

		RuleFor(x => x.CompletedByUserId)
			.NotEmpty().WithMessage("Tamamlayan kullanıcı zorunludur.");

		RuleFor(x => x.Notes)
			.MaximumLength(500).WithMessage("Notlar en fazla 500 karakter olabilir.");
	}
}


