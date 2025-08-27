using Application.Features.Commands;
using FluentValidation;

namespace Application.Validations;

public sealed class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
{
	public CreateAssetCommandValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Varlık adı zorunludur.")
			.MaximumLength(100).WithMessage("Varlık adı en fazla 100 karakter olabilir.");

		RuleFor(x => x.SerialNumber)
			.NotEmpty().WithMessage("Seri numarası zorunludur.")
			.MaximumLength(100).WithMessage("Seri numarası en fazla 100 karakter olabilir.");

		RuleFor(x => x.Brand)
			.NotEmpty().WithMessage("Marka zorunludur.")
			.MaximumLength(100).WithMessage("Marka en fazla 100 karakter olabilir.");

		RuleFor(x => x.Model)
			.NotEmpty().WithMessage("Model zorunludur.")
			.MaximumLength(100).WithMessage("Model en fazla 100 karakter olabilir.");

		RuleFor(x => x.AssetTag)
			.NotEmpty().WithMessage("Envanter etiketi zorunludur.")
			.MaximumLength(50).WithMessage("Envanter etiketi en fazla 50 karakter olabilir.");

		RuleFor(x => x.AssetTypeId)
			.NotEmpty().WithMessage("Varlık tipi zorunludur.");

		RuleFor(x => x.LocationId)
			.NotEmpty().WithMessage("Lokasyon zorunludur.");

		RuleFor(x => x.DepartmentId)
			.NotEmpty().WithMessage("Departman zorunludur.");

		RuleFor(x => x.PurchaseDate)
			.LessThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Satın alma tarihi gelecek bir tarih olamaz.");

		RuleFor(x => x.WarrantyExpiryDate)
			.Must((cmd, warranty) => !warranty.HasValue || warranty.Value >= cmd.PurchaseDate)
			.WithMessage("Garanti bitiş tarihi satın alma tarihinden önce olamaz.");

		RuleFor(x => x.Description)
			.MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

		RuleFor(x => x.CabinetId)
			.MaximumLength(50).When(x => !string.IsNullOrEmpty(x.CabinetId))
			.WithMessage("Cabinet ID en fazla 50 karakter olabilir.");
	}
}


