using Application.Features.Commands;
using FluentValidation;

namespace Application.Validations;

public sealed class AssignAssetToUserCommandValidator : AbstractValidator<AssignAssetToUserCommand>
{
    public AssignAssetToUserCommandValidator()
    {
        RuleFor(x => x.AssetId)
            .NotEmpty().WithMessage("Asset ID zorunludur.")
            .MaximumLength(50).WithMessage("Asset ID 50 karakterden uzun olamaz.");

        RuleFor(x => x.UserId)
            .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.UserId))
            .WithMessage("User ID 50 karakterden uzun olamaz.");
    }
}
