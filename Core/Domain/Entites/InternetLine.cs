using Domain.Entities;

public class InternetLine : BaseEntity
{
    public string LineNumber { get; set; }
    public string Provider { get; set; }
    public string Speed { get; set; }
    public DateTime ContractEndDate { get; set; }

    // Lokasyon
    public string LocationId { get; set; }
    public Location Location { get; set; }
    
}