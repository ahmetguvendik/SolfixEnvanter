namespace Application.Features.Results.StatisticsResults;

public class GetExpiringSslCertificatesQueryResult
{
    public string Id { get; set; }
    public string CommonName { get; set; }
    public string Provider { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool AutoRenew { get; set; }
    public string DomainName { get; set; }
    public string? Notes { get; set; }
    public int DaysUntilExpiration { get; set; }
}
