namespace Domain.Entities;

public class AssetType : BaseEntity
{
    public string Name { get; set; } 
    public ICollection<Asset> Assets { get; set; }
}