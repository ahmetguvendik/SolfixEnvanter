using Domain.Enums;

namespace Application.Features.Results.StatisticsResults;

public class GetDashboardSummaryQueryResult
{
    public int TotalAssets { get; set; }
    public int TotalUsers { get; set; }
    public int AssignedAssets { get; set; }
    public int UnassignedAssets { get; set; }
    public int CompletedMaintenances { get; set; }
    public int OverdueMaintenances { get; set; }
    public int UpcomingMaintenances { get; set; }
    public int TotalSslCertificates { get; set; }
    public int ExpiringSslCertificates { get; set; }
    public int TotalDomains { get; set; }
    public int ExpiringDomains { get; set; }
    public int TotalCabinets { get; set; }
    public int TotalInternetLines { get; set; }
    public int TotalNetworkInfos { get; set; }
    public Dictionary<AssetStatus, int> AssetsByStatus { get; set; }
    public Dictionary<MaintenanceStatus, int> MaintenancesByStatus { get; set; }
    public int ExpiredWarrantyAssets { get; set; }
    public int ExpiringWarrantyAssets { get; set; }
}
