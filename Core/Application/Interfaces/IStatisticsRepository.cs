using Domain.Entites;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces;

public interface IStatisticsRepository
{
    // Asset İstatistikleri
    Task<int> GetTotalAssetsCountAsync(CancellationToken cancellationToken = default);
    Task<Dictionary<AssetStatus, int>> GetAssetsByStatusAsync(CancellationToken cancellationToken = default);
    Task<Dictionary<string, int>> GetAssetsByDepartmentAsync(CancellationToken cancellationToken = default);
    Task<Dictionary<string, int>> GetAssetsByTypeAsync(CancellationToken cancellationToken = default);
    Task<Dictionary<string, int>> GetAssetsByLocationAsync(CancellationToken cancellationToken = default);
    Task<List<Asset>> GetAssetsWithExpiredWarrantyAsync(CancellationToken cancellationToken = default);
    Task<List<Asset>> GetAssetsWithWarrantyExpiringInDaysAsync(int days, CancellationToken cancellationToken = default);
    
    // User ve Assignment İstatistikleri
    Task<int> GetTotalUsersCountAsync(CancellationToken cancellationToken = default);
    Task<int> GetAssignedAssetsCountAsync(CancellationToken cancellationToken = default);
    Task<int> GetUnassignedAssetsCountAsync(CancellationToken cancellationToken = default);
    
    // Maintenance İstatistikleri
    Task<Dictionary<MaintenanceStatus, int>> GetMaintenancesByStatusAsync(CancellationToken cancellationToken = default);
    Task<List<MaintenanceRecord>> GetUpcomingMaintenancesAsync(int days, CancellationToken cancellationToken = default);
    Task<List<MaintenanceRecord>> GetOverdueMaintenancesAsync(CancellationToken cancellationToken = default);
    Task<int> GetCompletedMaintenancesCountAsync(CancellationToken cancellationToken = default);
    
    // SSL Certificate İstatistikleri
    Task<List<SslCertificate>> GetExpiringSslCertificatesAsync(int days, CancellationToken cancellationToken = default);
    Task<List<SslCertificate>> GetExpiredSslCertificatesAsync(CancellationToken cancellationToken = default);
    Task<int> GetTotalSslCertificatesCountAsync(CancellationToken cancellationToken = default);
    
    // Domain İstatistikleri
    Task<List<DomainName>> GetExpiringDomainsAsync(int days, CancellationToken cancellationToken = default);
    Task<List<DomainName>> GetExpiredDomainsAsync(CancellationToken cancellationToken = default);
    Task<int> GetTotalDomainsCountAsync(CancellationToken cancellationToken = default);
    
    // Network İstatistikleri
    Task<Dictionary<AssetNetworkInfoStatues, int>> GetNetworkInfoByStatusAsync(CancellationToken cancellationToken = default);
    Task<int> GetTotalNetworkInfoCountAsync(CancellationToken cancellationToken = default);
    
    // Cabinet İstatistikleri
    Task<Dictionary<string, int>> GetAssetsByCabinetAsync(CancellationToken cancellationToken = default);
    Task<int> GetTotalCabinetsCountAsync(CancellationToken cancellationToken = default);
    
    // Internet Lines İstatistikleri
    Task<int> GetTotalInternetLinesCountAsync(CancellationToken cancellationToken = default);
    
    // Genel Özet İstatistikleri
    Task<Dictionary<string, object>> GetDashboardSummaryAsync(CancellationToken cancellationToken = default);
}