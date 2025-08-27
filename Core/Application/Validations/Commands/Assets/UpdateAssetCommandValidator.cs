using Application.Features.Commands;
using FluentValidation;

namespace Application.Validations;

public sealed class UpdateAssetCommandValidator : AbstractValidator<UpdateAssetCommand>
{
	public UpdateAssetCommandValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty().WithMessage("ID zorunludur.");

		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Varlık adı zorunludur.")
			.MaximumLength(100).WithMessage("Varlık adı 100 karakterden uzun olamaz.");

		RuleFor(x => x.SerialNumber)
			.NotEmpty().WithMessage("Seri numarası zorunludur.")
			.MaximumLength(50).WithMessage("Seri numarası 50 karakterden uzun olamaz.");

		RuleFor(x => x.Brand)
			.NotEmpty().WithMessage("Marka zorunludur.")
			.MaximumLength(50).WithMessage("Marka 50 karakterden uzun olamaz.");

		RuleFor(x => x.Model)
			.NotEmpty().WithMessage("Model zorunludur.")
			.MaximumLength(50).WithMessage("Model 50 karakterden uzun olamaz.");

		RuleFor(x => x.AssetTag)
			.NotEmpty().WithMessage("Varlık etiketi zorunludur.")
			.MaximumLength(20).WithMessage("Varlık etiketi 20 karakterden uzun olamaz.");

		RuleFor(x => x.AssetTypeId)
			.NotEmpty().WithMessage("Varlık tipi zorunludur.");

		RuleFor(x => x.LocationId)
			.NotEmpty().WithMessage("Lokasyon zorunludur.");

		RuleFor(x => x.DepartmentId)
			.NotEmpty().WithMessage("Departman zorunludur.");
		
		RuleFor(x => x.Description)
			.MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Description))
			.WithMessage("Açıklama 500 karakterden uzun olamaz.");

		RuleFor(x => x.CabinetId)
			.MaximumLength(50).When(x => !string.IsNullOrEmpty(x.CabinetId))
			.WithMessage("Cabinet ID 50 karakterden uzun olamaz.");
	}
}
