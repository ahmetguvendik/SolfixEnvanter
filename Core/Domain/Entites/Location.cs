namespace Domain.Entites;

public class Location : BaseEntity
{
    public string Name { get; set; } 
    public ICollection<Asset> Assets { get; set; }
}
