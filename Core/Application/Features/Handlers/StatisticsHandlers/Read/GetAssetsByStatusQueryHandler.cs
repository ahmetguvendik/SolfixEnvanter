using Application.Features.Queries.StatisticsQueries;
using Application.Features.Results.StatisticsResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.StatisticsHandlers.Read;

public class GetAssetsByStatusQueryHandler : IRequestHandler<GetAssetsByStatusQuery, GetAssetsByStatusQueryResult>
{
    private readonly IStatisticsRepository _statisticsRepository;

    public GetAssetsByStatusQueryHandler(IStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }

    public async Task<GetAssetsByStatusQueryResult> Handle(GetAssetsByStatusQuery request, CancellationToken cancellationToken)
    {
        var assetsByStatus = await _statisticsRepository.GetAssetsByStatusAsync(cancellationToken);
        
        return new GetAssetsByStatusQueryResult
        {
            AssetsByStatus = assetsByStatus
        };
    }
}
