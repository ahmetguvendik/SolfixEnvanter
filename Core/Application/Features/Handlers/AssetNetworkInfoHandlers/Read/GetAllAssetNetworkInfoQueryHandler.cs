using Application.Features.Queries.AssetNetworkInfoQueries;
using Application.Features.Results.AssetNetworkInfoResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.AssetNetworkInfoHandlers.Read;

public class GetAllAssetNetworkInfoQueryHandler : IRequestHandler<GetAllAssetNetworkInfoQuery, List<GetAllAssetNetworkInfoQueryResult>>
{
    private readonly IAssetNetworkInfoRepository _assetNetworkInfoRepository;

    public GetAllAssetNetworkInfoQueryHandler(IAssetNetworkInfoRepository assetNetworkInfoRepository)
    {
         _assetNetworkInfoRepository = assetNetworkInfoRepository;
    }
    
    public async Task<List<GetAllAssetNetworkInfoQueryResult>> Handle(GetAllAssetNetworkInfoQuery request, CancellationToken cancellationToken)
    {
        var values = await _assetNetworkInfoRepository.GetAllAssetNetworkInfoWithAssets();
        return values.Select(x=> new  GetAllAssetNetworkInfoQueryResult
        {
            Id = x.Id,
            AssetName = x.Asset.Name,
            IPAddress = x.IPAddress,
            MacAddress = x.MacAddress,
            Status = x.Status,
        }).ToList();
    }
}