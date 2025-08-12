using Application.Enums;

namespace Application.DTOs;

public class AssetInCabinetDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string AssetTag { get; set; }
    public AssetStatus AssetStatus { get; set; }
    public string AssetTypeName { get; set; }
}