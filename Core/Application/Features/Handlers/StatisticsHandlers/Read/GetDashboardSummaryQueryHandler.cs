using Application.Features.Queries.StatisticsQueries;
using Application.Features.Results.StatisticsResults;
using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Features.Handlers.StatisticsHandlers.Read;

public class GetDashboardSummaryQueryHandler : IRequestHandler<GetDashboardSummaryQuery, GetDashboardSummaryQueryResult>
{
    private readonly IStatisticsRepository _statisticsRepository;

    public GetDashboardSummaryQueryHandler(IStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }

    public async Task<GetDashboardSummaryQueryResult> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
    {
        var summary = await _statisticsRepository.GetDashboardSummaryAsync(cancellationToken);
        
        return new GetDashboardSummaryQueryResult
        {
            TotalAssets = (int)summary["TotalAssets"],
            TotalUsers = (int)summary["TotalUsers"],
            AssignedAssets = (int)summary["AssignedAssets"],
            UnassignedAssets = (int)summary["UnassignedAssets"],
            CompletedMaintenances = (int)summary["CompletedMaintenances"],
            OverdueMaintenances = (int)summary["OverdueMaintenances"],
            UpcomingMaintenances = (int)summary["UpcomingMaintenances"],
            TotalSslCertificates = (int)summary["TotalSslCertificates"],
            ExpiringSslCertificates = (int)summary["ExpiringSslCertificates"],
            TotalDomains = (int)summary["TotalDomains"],
            ExpiringDomains = (int)summary["ExpiringDomains"],
            TotalCabinets = (int)summary["TotalCabinets"],
            TotalInternetLines = (int)summary["TotalInternetLines"],
            TotalNetworkInfos = (int)summary["TotalNetworkInfos"],
            AssetsByStatus = (Dictionary<AssetStatus, int>)summary["AssetsByStatus"],
            MaintenancesByStatus = (Dictionary<Domain.Enums.MaintenanceStatus, int>)summary["MaintenancesByStatus"],
            ExpiredWarrantyAssets = (int)summary["ExpiredWarrantyAssets"],
            ExpiringWarrantyAssets = (int)summary["ExpiringWarrantyAssets"]
        };
    }
}
