namespace Application.Features.Results.StatisticsResults;

public class GetExpiringDomainsQueryResult
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Registrar { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool AutoRenew { get; set; }
    public string? HostingProvider { get; set; }
    public string? Notes { get; set; }
    public int DaysUntilExpiration { get; set; }
}
