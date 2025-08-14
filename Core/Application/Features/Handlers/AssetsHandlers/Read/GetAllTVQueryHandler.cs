using Application.Features.Queries.AssetQueries;
using Application.Features.Results.AssetResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.AssetsHandlers.Read;

public class GetAllTVQueryHandler  : IRequestHandler<GetAllTVQuery, List<GetAllTVQueryResult>>
{
    private readonly IAssetRepository _assetRepository;

    public GetAllTVQueryHandler(IAssetRepository assetRepository)
    {
         _assetRepository = assetRepository;
    }
    
    public async Task<List<GetAllTVQueryResult>> Handle(GetAllTVQuery request, CancellationToken cancellationToken)
    {
        var values = await _assetRepository.GetAllByAssetTypeIdAsync("13", cancellationToken);
        return values.Select(x=> new GetAllTVQueryResult()
        {
            Name = x.Name,
            SerialNumber = x.SerialNumber,
            Brand = x.Brand,
            Model = x.Model,
            AssetTag = x.AssetTag,
            AssetTypeName = x.AssetType.Name,
            LocationName = x.Location.Name,
            AssignedToUserName = x.AssignedToUser?.FullName,
            PurchaseDate = x.PurchaseDate,
            WarrantyExpiryDate = x.WarrantyExpiryDate,
            Status = x.Status,
            Description = x.Description,
            DepartmentName = x.Department.Name,
        }).ToList();
    }
}