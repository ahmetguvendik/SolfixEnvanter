namespace Application.Features.Results.SslCertificateResults;

public class GetAllSslCertificateQueryResult
{
    public string Id { get; set; }
    public string CommonName { get; set; } // Alan adı veya wildcard (*.example.com)
    public string Provider { get; set; } // Let's Encrypt, Sectigo vb.
    public DateTime StartDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool AutoRenew { get; set; }
    // Domain ile ilişki
    public string DomainName { get; set; }  
    public string? Notes { get; set; }
}   