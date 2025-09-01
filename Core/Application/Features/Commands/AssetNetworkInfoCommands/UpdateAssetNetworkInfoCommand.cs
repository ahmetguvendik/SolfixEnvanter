using Domain.Enums;
using MediatR;

namespace Application.Features.Commands;

public class UpdateAssetNetworkInfoCommand : IRequest
{
    public string Id { get; set; }
    public string AssetId { get; set; }
    public string IPAddress { get; set; }
    public string MacAddress { get; set; }
    public AssetNetworkInfoStatues Status { get; set; } 

}
