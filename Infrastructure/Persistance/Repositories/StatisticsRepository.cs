using Application.Interfaces;
using Domain.Entities;
using Domain.Entites;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StatisticsRepository : IStatisticsRepository
{
    private readonly SolfixEnvanterDbContext _dbContext;

    public StatisticsRepository(SolfixEnvanterDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Asset İstatistikleri
    public async Task<int> GetTotalAssetsCountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Assets.CountAsync(cancellationToken);
    }

    public async Task<Dictionary<AssetStatus, int>> GetAssetsByStatusAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Assets
            .GroupBy(a => a.Status)
            .ToDictionaryAsync(g => g.Key, g => g.Count(), cancellationToken);
    }

    public async Task<Dictionary<string, int>> GetAssetsByDepartmentAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Assets
            .Include(a => a.Department)
            .GroupBy(a => a.Department.Name)
            .ToDictionaryAsync(g => g.Key, g => g.Count(), cancellationToken);
    }

    public async Task<Dictionary<string, int>> GetAssetsByTypeAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Assets
            .Include(a => a.AssetType)
            .GroupBy(a => a.AssetType.Name)
            .ToDictionaryAsync(g => g.Key, g => g.Count(), cancellationToken);
    }

    public async Task<Dictionary<string, int>> GetAssetsByLocationAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Assets
            .Include(a => a.Location)
            .GroupBy(a => a.Location.Name)
            .ToDictionaryAsync(g => g.Key, g => g.Count(), cancellationToken);
    }

    public async Task<List<Asset>> GetAssetsWithExpiredWarrantyAsync(CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        return await _dbContext.Assets
            .AsNoTracking()
            .Include(a => a.AssetType)
            .Include(a => a.Department)
            .Include(a => a.Location)
            .Where(a => a.WarrantyExpiryDate.HasValue && a.WarrantyExpiryDate.Value < today)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Asset>> GetAssetsWithWarrantyExpiringInDaysAsync(int days, CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        var targetDate = today.AddDays(days);
        return await _dbContext.Assets
            .AsNoTracking()
            .Include(a => a.AssetType)
            .Include(a => a.Department)
            .Include(a => a.Location)
            .Where(a => a.WarrantyExpiryDate.HasValue && 
                       a.WarrantyExpiryDate.Value >= today && 
                       a.WarrantyExpiryDate.Value <= targetDate)
            .ToListAsync(cancellationToken);
    }

    // User ve Assignment İstatistikleri
    public async Task<int> GetTotalUsersCountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.CountAsync(cancellationToken);
    }

    public async Task<int> GetAssignedAssetsCountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Assets
            .Where(a => a.AssignedToUserId != null)
            .CountAsync(cancellationToken);
    }

    public async Task<int> GetUnassignedAssetsCountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Assets
            .Where(a => a.AssignedToUserId == null)
            .CountAsync(cancellationToken);
    }

    // Maintenance İstatistikleri
    public async Task<Dictionary<MaintenanceStatus, int>> GetMaintenancesByStatusAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.MaintenanceRecords
            .GroupBy(m => m.Status)
            .ToDictionaryAsync(g => g.Key, g => g.Count(), cancellationToken);
    }

    public async Task<List<MaintenanceRecord>> GetUpcomingMaintenancesAsync(int days, CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        var targetDate = today.AddDays(days);
        return await _dbContext.MaintenanceRecords
            .AsNoTracking()
            .Include(m => m.MaintenanceType)
            .Include(m => m.CompletedByUser)
            .Where(m => m.Status == MaintenanceStatus.Scheduled && 
                       m.ScheduledDate >= today && 
                       m.ScheduledDate <= targetDate)
            .OrderBy(m => m.ScheduledDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<MaintenanceRecord>> GetOverdueMaintenancesAsync(CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        return await _dbContext.MaintenanceRecords
            .AsNoTracking()
            .Include(m => m.MaintenanceType)
            .Where(m => m.Status == MaintenanceStatus.Scheduled && m.ScheduledDate < today)
            .OrderBy(m => m.ScheduledDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetCompletedMaintenancesCountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.MaintenanceRecords
            .Where(m => m.Status == MaintenanceStatus.Completed)
            .CountAsync(cancellationToken);
    }

    // SSL Certificate İstatistikleri
    public async Task<List<SslCertificate>> GetExpiringSslCertificatesAsync(int days, CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        var targetDate = today.AddDays(days);
        return await _dbContext.SslCertificates
            .AsNoTracking()
            .Include(s => s.Domain)
            .Where(s => s.ExpirationDate >= today && s.ExpirationDate <= targetDate)
            .OrderBy(s => s.ExpirationDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<SslCertificate>> GetExpiredSslCertificatesAsync(CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        return await _dbContext.SslCertificates
            .AsNoTracking()
            .Include(s => s.Domain)
            .Where(s => s.ExpirationDate < today)
            .OrderBy(s => s.ExpirationDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetTotalSslCertificatesCountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SslCertificates.CountAsync(cancellationToken);
    }

    // Domain İstatistikleri
    public async Task<List<DomainName>> GetExpiringDomainsAsync(int days, CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        var targetDate = today.AddDays(days);
        return await _dbContext.DomainNames
            .AsNoTracking()
            .Where(d => d.ExpirationDate >= today && d.ExpirationDate <= targetDate)
            .OrderBy(d => d.ExpirationDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<DomainName>> GetExpiredDomainsAsync(CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        return await _dbContext.DomainNames
            .AsNoTracking()
            .Where(d => d.ExpirationDate < today)
            .OrderBy(d => d.ExpirationDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetTotalDomainsCountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.DomainNames.CountAsync(cancellationToken);
    }

    // Network İstatistikleri
    public async Task<Dictionary<AssetNetworkInfoStatues, int>> GetNetworkInfoByStatusAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.AssetNetworkInfos
            .GroupBy(n => n.Status)
            .ToDictionaryAsync(g => g.Key, g => g.Count(), cancellationToken);
    }

    public async Task<int> GetTotalNetworkInfoCountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.AssetNetworkInfos.CountAsync(cancellationToken);
    }

    // Cabinet İstatistikleri
    public async Task<Dictionary<string, int>> GetAssetsByCabinetAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Assets
            .Include(a => a.Cabinet)
            .Where(a => a.Cabinet != null)
            .GroupBy(a => a.Cabinet.Name)
            .ToDictionaryAsync(g => g.Key, g => g.Count(), cancellationToken);
    }

    public async Task<int> GetTotalCabinetsCountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Cabinets.CountAsync(cancellationToken);
    }

    // Internet Lines İstatistikleri
    public async Task<int> GetTotalInternetLinesCountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.InternetLines.CountAsync(cancellationToken);
    }

    // Genel Özet İstatistikleri
    public async Task<Dictionary<string, object>> GetDashboardSummaryAsync(CancellationToken cancellationToken = default)
    {
        var summary = new Dictionary<string, object>();

        // Temel sayılar
        summary["TotalAssets"] = await GetTotalAssetsCountAsync(cancellationToken);
        summary["TotalUsers"] = await GetTotalUsersCountAsync(cancellationToken);
        summary["AssignedAssets"] = await GetAssignedAssetsCountAsync(cancellationToken);
        summary["UnassignedAssets"] = await GetUnassignedAssetsCountAsync(cancellationToken);
        
        // Bakım istatistikleri
        summary["CompletedMaintenances"] = await GetCompletedMaintenancesCountAsync(cancellationToken);
        summary["OverdueMaintenances"] = (await GetOverdueMaintenancesAsync(cancellationToken)).Count;
        summary["UpcomingMaintenances"] = (await GetUpcomingMaintenancesAsync(30, cancellationToken)).Count;
        
        // SSL ve Domain
        summary["TotalSslCertificates"] = await GetTotalSslCertificatesCountAsync(cancellationToken);
        summary["ExpiringSslCertificates"] = (await GetExpiringSslCertificatesAsync(30, cancellationToken)).Count;
        summary["TotalDomains"] = await GetTotalDomainsCountAsync(cancellationToken);
        summary["ExpiringDomains"] = (await GetExpiringDomainsAsync(30, cancellationToken)).Count;
        
        // Diğer
        summary["TotalCabinets"] = await GetTotalCabinetsCountAsync(cancellationToken);
        summary["TotalInternetLines"] = await GetTotalInternetLinesCountAsync(cancellationToken);
        summary["TotalNetworkInfos"] = await GetTotalNetworkInfoCountAsync(cancellationToken);
        
        // Durum dağılımları
        summary["AssetsByStatus"] = await GetAssetsByStatusAsync(cancellationToken);
        summary["MaintenancesByStatus"] = await GetMaintenancesByStatusAsync(cancellationToken);
        
        // Garanti durumu
        summary["ExpiredWarrantyAssets"] = (await GetAssetsWithExpiredWarrantyAsync(cancellationToken)).Count;
        summary["ExpiringWarrantyAssets"] = (await GetAssetsWithWarrantyExpiringInDaysAsync(30, cancellationToken)).Count;

        return summary;
    }
}
