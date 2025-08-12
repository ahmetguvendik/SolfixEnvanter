using Application.Features.Queries.AssetQueries;
using Application.Features.Results.AssetResults;
using Application.Interfaces;
using Domain.Entites;
using MediatR;

namespace Application.Features.Handlers.AssetsHandlers.Read;

public class GetAllAssetsQueryHandler  : IRequestHandler<GetAllAssetsQuery, List<GetAllAssetsQueryResult>>
{
    private readonly IGenericRepository<Asset> _assetRepository;

    public GetAllAssetsQueryHandler(IGenericRepository<Asset> assetRepository)
    {
         _assetRepository = assetRepository;
    }
    public async Task<List<GetAllAssetsQueryResult>> Handle(GetAllAssetsQuery request, CancellationToken cancellationToken)
    {
        var values = await _assetRepository.GetAllAsync();
        return values.Select(x=> new GetAllAssetsQueryResult()
        {
            Id = x.Id,
            Name = x.Name,
            SeriNo = x.SerialNumber
        }).ToList();
    }
}