namespace Application.Features.Results.DomainNameResults;

public class GetAllDomainNameQueryResult
{
    public string Id { get; set; }
    public string Name { get; set; } // örn: example.com
    public string Registrar { get; set; } // Godaddy, Namecheap vb.
    public DateTime RegistrationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool AutoRenew { get; set; }

    // Hosting Bilgileri (Opsiyonel)
    public string? HostingProvider { get; set; }
    public string? HostingPlan { get; set; }
    public string? ServerIP { get; set; }
    public DateTime? HostingExpirationDate { get; set; }
    
    // Ek Notlar
    public string? Notes { get; set; }
}