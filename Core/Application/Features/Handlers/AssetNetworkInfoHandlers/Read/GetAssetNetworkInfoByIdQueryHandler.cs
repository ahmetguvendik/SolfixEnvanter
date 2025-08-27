using Application.Features.Queries.AssetNetworkInfoQueries;
using Application.Features.Results.AssetNetworkInfoResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.AssetNetworkInfoHandlers.Read;

public class GetAssetNetworkInfoByIdQueryHandler : IRequestHandler<GetAssetNetworkInfoByIdQuery, GetAssetNetworkInfoByIdQueryResult>
{
    private readonly IAssetNetworkInfoRepository _assetNetworkInfoRepository;

    public GetAssetNetworkInfoByIdQueryHandler(IAssetNetworkInfoRepository assetNetworkInfoRepository)
    {
        _assetNetworkInfoRepository = assetNetworkInfoRepository;
    }
    
    public async Task<GetAssetNetworkInfoByIdQueryResult> Handle(GetAssetNetworkInfoByIdQuery request, CancellationToken cancellationToken)
    {
        var assetNetworkInfo = await _assetNetworkInfoRepository.GetAssetNetworkInfoWithAssetById(request.Id);
        
        if (assetNetworkInfo == null)
            throw new Exception("Asset network info not found");
            
        return new GetAssetNetworkInfoByIdQueryResult
        {
            Id = assetNetworkInfo.Id,
            AssetId = assetNetworkInfo.AssetId,
            AssetName = assetNetworkInfo.Asset.Name,
            IPAddress = assetNetworkInfo.IPAddress,
            MacAddress = assetNetworkInfo.MacAddress,
            Status = assetNetworkInfo.Status,
        };
    }
}
