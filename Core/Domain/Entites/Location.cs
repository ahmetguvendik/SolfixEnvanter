using Domain.Entites;

namespace Domain.Entities;

public class Location : BaseEntity
{
    public string Name { get; set; } 
    public ICollection<Asset> Assets { get; set; }
    public ICollection<Cabinet> Cabinets { get; set; }
}
