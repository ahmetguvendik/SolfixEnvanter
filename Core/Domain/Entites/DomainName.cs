using Domain.Entities;

namespace Domain.Entites;

public class DomainName : BaseEntity
{
    // Domain Bilgileri
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
    public ICollection<SslCertificate> SslCertificates { get; set; }
    // Ek Notlar
    public string? Notes { get; set; }
}