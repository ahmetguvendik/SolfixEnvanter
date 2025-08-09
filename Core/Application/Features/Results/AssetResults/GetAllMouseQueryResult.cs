using Application.Enums;

namespace Application.Features.Results.AssetResults;

public class GetAllMouseQueryResult
{
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string AssetTag { get; set; } 
    public string AssetTypeName { get; set; }
    public string LocationName { get; set; }
    public string? AssignedToUserName { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime? WarrantyExpiryDate { get; set; }
    public AssetStatus Status { get; set; } // Enum: Aktif, Arızalı, İade, Hurda
    public string? Description { get; set; }
    public string DepartmentName { get; set; }  
}