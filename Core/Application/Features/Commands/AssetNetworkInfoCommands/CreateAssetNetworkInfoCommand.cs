using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.AssetNetworkInfoCommands;

public class CreateAssetNetworkInfoCommand : IRequest
{
    public string AssetId { get; set; }
    public string IPAddress { get; set; }
    public string MacAddress { get; set; }
}