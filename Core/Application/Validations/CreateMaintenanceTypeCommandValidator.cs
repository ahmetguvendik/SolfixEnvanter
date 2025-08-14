using Application.Features.Commands.MaintenanceTypeCommands;
using FluentValidation;

namespace Application.Validations;

public sealed class CreateMaintenanceTypeCommandValidator : AbstractValidator<CreateMaintenanceTypeCommand>
{
	public CreateMaintenanceTypeCommandValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Bakım tipi adı zorunludur.")
			.MaximumLength(100).WithMessage("Bakım tipi adı en fazla 100 karakter olabilir.");
	}
}


