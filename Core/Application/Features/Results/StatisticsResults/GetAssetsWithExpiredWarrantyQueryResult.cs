using Domain.Enums;

namespace Application.Features.Results.StatisticsResults;

public class GetAssetsWithExpiredWarrantyQueryResult
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string AssetTag { get; set; }
    public string AssetTypeName { get; set; }
    public string LocationName { get; set; }
    public string DepartmentName { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime? WarrantyExpiryDate { get; set; }
    public AssetStatus Status { get; set; }
    public int DaysSinceExpired { get; set; }
}
