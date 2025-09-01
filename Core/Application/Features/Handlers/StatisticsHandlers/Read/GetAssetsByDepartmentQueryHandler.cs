using Application.Features.Queries.StatisticsQueries;
using Application.Features.Results.StatisticsResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.StatisticsHandlers.Read;

public class GetAssetsByDepartmentQueryHandler : IRequestHandler<GetAssetsByDepartmentQuery, GetAssetsByDepartmentQueryResult>
{
    private readonly IStatisticsRepository _statisticsRepository;

    public GetAssetsByDepartmentQueryHandler(IStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }

    public async Task<GetAssetsByDepartmentQueryResult> Handle(GetAssetsByDepartmentQuery request, CancellationToken cancellationToken)
    {
        var assetsByDepartment = await _statisticsRepository.GetAssetsByDepartmentAsync(cancellationToken);
        
        return new GetAssetsByDepartmentQueryResult
        {
            AssetsByDepartment = assetsByDepartment
        };
    }
}
