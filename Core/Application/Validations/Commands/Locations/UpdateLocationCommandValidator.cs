using Application.Features.Commands;
using FluentValidation;

namespace Application.Validations;

public sealed class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
{
	public UpdateLocationCommandValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty().WithMessage("ID zorunludur.");

		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Lokasyon adı zorunludur.")
			.MaximumLength(100).WithMessage("Lokasyon adı 100 karakterden uzun olamaz.");
	}
}
