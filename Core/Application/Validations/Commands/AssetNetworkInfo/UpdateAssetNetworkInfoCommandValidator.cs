using Application.Features.Commands;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Validations;

public sealed class UpdateAssetNetworkInfoCommandValidator : AbstractValidator<UpdateAssetNetworkInfoCommand>
{
	public UpdateAssetNetworkInfoCommandValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty().WithMessage("ID zorunludur.");

		RuleFor(x => x.AssetId)
			.NotEmpty().WithMessage("Varlık ID zorunludur.");

		RuleFor(x => x.IPAddress)
			.NotEmpty().WithMessage("IP adresi zorunludur.")
			.Matches(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")
			.WithMessage("Geçerli bir IP adresi giriniz (örn: 192.168.1.1).");

		RuleFor(x => x.MacAddress)
			.NotEmpty().WithMessage("MAC adresi zorunludur.")
			.Matches(@"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$")
			.WithMessage("Geçerli bir MAC adresi giriniz (örn: 00:1B:44:11:3A:B7).");

		RuleFor(x => x.Status)
			.IsInEnum().WithMessage("Geçerli bir durum seçiniz.");
	}
}
