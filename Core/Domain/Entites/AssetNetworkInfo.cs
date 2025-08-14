namespace Domain.Entities;

public class AssetNetworkInfo : BaseEntity
{
    public string AssetId { get; set; }
    public Asset Asset { get; set; }
    public string IPAddress { get; set; }
    public string MacAddress { get; set; }
}