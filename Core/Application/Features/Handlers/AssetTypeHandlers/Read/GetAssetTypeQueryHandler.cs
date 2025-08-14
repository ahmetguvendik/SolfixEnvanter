using Application.Features.Queries.AssetTypeQueries;
using Application.Features.Results.AssetTypeResults;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Handlers.AssetTypeHandlers.Read;

public class GetAssetTypeQueryHandler : IRequestHandler<GetAssetTypeQuery, List<GetAssetTypeQueryResults>>
{
    private readonly IGenericRepository<AssetType> _assetTypeRepository;

    public GetAssetTypeQueryHandler(IGenericRepository<AssetType> assetTypeRepository)
    {
         _assetTypeRepository = assetTypeRepository;
    }
    
    public async Task<List<GetAssetTypeQueryResults>> Handle(GetAssetTypeQuery request, CancellationToken cancellationToken)
    {
        var results = await _assetTypeRepository.GetAllAsync();
        return results.Select(x => new GetAssetTypeQueryResults()
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();
    }
}