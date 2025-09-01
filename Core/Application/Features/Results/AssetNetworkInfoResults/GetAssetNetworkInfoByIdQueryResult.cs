using Domain.Enums;

namespace Application.Features.Results.AssetNetworkInfoResults;

public class GetAssetNetworkInfoByIdQueryResult
{
    public string Id { get; set; }
    public string AssetId { get; set; }
    public string AssetName { get; set; }
    public string IPAddress { get; set; }
    public string MacAddress { get; set; }
    public AssetNetworkInfoStatues Status { get; set; } 

}
