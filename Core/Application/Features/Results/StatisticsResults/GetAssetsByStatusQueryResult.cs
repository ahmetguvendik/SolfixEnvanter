using Domain.Enums;

namespace Application.Features.Results.StatisticsResults;

public class GetAssetsByStatusQueryResult
{
    public Dictionary<AssetStatus, int> AssetsByStatus { get; set; }
}
