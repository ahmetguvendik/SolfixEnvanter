using Application.Features.Commands;
using FluentValidation;

namespace Application.Validations;

public sealed class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
	public CreateLocationCommandValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Lokasyon adı zorunludur.")
			.MaximumLength(100).WithMessage("Lokasyon adı en fazla 100 karakter olabilir.");
	}
}


