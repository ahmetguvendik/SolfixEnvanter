namespace Domain.Entites;

public class AssetType : BaseEntity
{
    public string Name { get; set; } // Örn: "Laptop", "Switch"
    public ICollection<Asset> Assets { get; set; }
}