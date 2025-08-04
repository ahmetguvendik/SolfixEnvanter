namespace Domain.Entites;

public class Department : BaseEntity
{
    public string Name { get; set; }
    // İlişkiler
    public ICollection<AppUser> Users { get; set; }
    public ICollection<Asset> Assets { get; set; }
}
