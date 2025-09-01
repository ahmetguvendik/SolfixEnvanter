using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Results.AssetResults;

public class GetAssetByIdQueryResult
{
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string AssetTag { get; set; } // Envanter numarası
    // Kategorilendirme
    public string AssetTypeName { get; set; }
    // Lokasyon bilgisi
    public string LocationName { get; set; }
    // Zimmetli personel
    public string? AssignedToUserName { get; set; }
    // Satın alma / garanti bilgisi
    public DateTime PurchaseDate { get; set; }
    public DateTime? WarrantyExpiryDate { get; set; }
    // Cihaz durumu
    public AssetStatus Status { get; set; } // Enum: Aktif, Arızalı, İade, Hurda
    public string? Description { get; set; }
    public string DepartmentName { get; set; }
    public string? CabinetName { get; set; }  

}