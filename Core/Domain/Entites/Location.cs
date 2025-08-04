namespace Domain.Entites;

public class Location : BaseEntity
{
    public string Name { get; set; } // Örn: "Dalaman Airport - IT Room"

    public ICollection<Asset> Assets { get; set; }
}
