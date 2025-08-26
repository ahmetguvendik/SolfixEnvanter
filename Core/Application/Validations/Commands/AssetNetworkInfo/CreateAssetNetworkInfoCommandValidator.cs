using Application.Features.Commands.AssetNetworkInfoCommands;
using FluentValidation;

namespace Application.Validations;

public sealed class CreateAssetNetworkInfoCommandValidator : AbstractValidator<CreateAssetNetworkInfoCommand>
{
	public CreateAssetNetworkInfoCommandValidator()
	{
		RuleFor(x => x.AssetId)
			.NotEmpty().WithMessage("Varlık zorunludur.");

		RuleFor(x => x.IPAddress)
			.NotEmpty().WithMessage("IP adresi zorunludur.")
			.Matches(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")
			.WithMessage("Geçerli bir IPv4 adresi giriniz.");

		RuleFor(x => x.MacAddress)
			.NotEmpty().WithMessage("MAC adresi zorunludur.")
			.Matches(@"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$")
			.WithMessage("Geçerli bir MAC adresi giriniz.");
	}
}


