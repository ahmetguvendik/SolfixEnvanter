using Domain.Entites;

public class SslCertificate : BaseEntity
{
    public string CommonName { get; set; } // Alan adı veya wildcard (*.example.com)
    public string Provider { get; set; } // Let's Encrypt, Sectigo vb.
    public DateTime StartDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool AutoRenew { get; set; }
    // Domain ile ilişki
    public string DomainId { get; set; }
    public DomainName Domain { get; set; }
    public string? Notes { get; set; }
}