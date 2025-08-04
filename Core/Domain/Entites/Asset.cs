using Application.Enums;

namespace Domain.Entites;

public class Asset : BaseEntity
{

        
        // Temel bilgiler
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string AssetTag { get; set; } // Envanter numarası
    
        // Kategorilendirme
        public int AssetTypeId { get; set; }
        public AssetType AssetType { get; set; }

        // Lokasyon bilgisi
        public int LocationId { get; set; }
        public Location Location { get; set; }

        // Zimmetli personel
        public string? AssignedToUserId { get; set; }
        public AppUser? AssignedToUser { get; set; }


        // Satın alma / garanti bilgisi
        public DateTime PurchaseDate { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }

        // Cihaz durumu
        public AssetStatus Status { get; set; } // Enum: Aktif, Arızalı, İade, Hurda
        public string? Description { get; set; }


}
    