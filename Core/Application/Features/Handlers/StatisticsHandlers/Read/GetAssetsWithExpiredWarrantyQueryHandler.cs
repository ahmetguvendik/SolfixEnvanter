using Application.Features.Queries.StatisticsQueries;
using Application.Features.Results.StatisticsResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.StatisticsHandlers.Read;

public class GetAssetsWithExpiredWarrantyQueryHandler : IRequestHandler<GetAssetsWithExpiredWarrantyQuery, List<GetAssetsWithExpiredWarrantyQueryResult>>
{
    private readonly IStatisticsRepository _statisticsRepository;

    public GetAssetsWithExpiredWarrantyQueryHandler(IStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }

    public async Task<List<GetAssetsWithExpiredWarrantyQueryResult>> Handle(GetAssetsWithExpiredWarrantyQuery request, CancellationToken cancellationToken)
    {
        var assetsWithExpiredWarranty = await _statisticsRepository.GetAssetsWithExpiredWarrantyAsync(cancellationToken);
        var today = DateTime.Today;
        
        return assetsWithExpiredWarranty.Select(a => new GetAssetsWithExpiredWarrantyQueryResult
        {
            Id = a.Id,
            Name = a.Name,
            SerialNumber = a.SerialNumber,
            Brand = a.Brand,
            Model = a.Model,
            AssetTag = a.AssetTag,
            AssetTypeName = a.AssetType.Name,
            LocationName = a.Location.Name,
            DepartmentName = a.Department.Name,
            PurchaseDate = a.PurchaseDate,
            WarrantyExpiryDate = a.WarrantyExpiryDate,
            Status = a.Status,
            DaysSinceExpired = a.WarrantyExpiryDate.HasValue ? (today - a.WarrantyExpiryDate.Value).Days : 0
        }).ToList();
    }
}
