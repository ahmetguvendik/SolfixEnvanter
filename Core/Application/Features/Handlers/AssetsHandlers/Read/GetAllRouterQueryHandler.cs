using Application.Features.Queries.AssetQueries;
using Application.Features.Results.AssetResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.AssetsHandlers.Read;

public class GetAllRouterQueryHandler : IRequestHandler<GetAllRouterQuery, List<GetAllRouterQueryResult>>
{
    private readonly IAssetRepository _assetRepository;

    public GetAllRouterQueryHandler(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }
    public async Task<List<GetAllRouterQueryResult>> Handle(GetAllRouterQuery request, CancellationToken cancellationToken)
    {
        var values = await _assetRepository.GetAllByAssetTypeIdAsync("7");
        return values.Select(x=> new GetAllRouterQueryResult()
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