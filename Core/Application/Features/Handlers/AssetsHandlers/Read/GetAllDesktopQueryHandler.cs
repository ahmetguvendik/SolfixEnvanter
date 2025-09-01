using Application.Features.Queries.AssetQueries;
using Application.Features.Results.AssetResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.AssetsHandlers.Read;

public class GetAllDesktopQueryHandler : IRequestHandler<GetAllDesktopQuery, List<GetAllDesktopQueryResult>>
{
    private readonly IAssetRepository _assetRepository;

    public GetAllDesktopQueryHandler(IAssetRepository assetRepository)
    {
         _assetRepository = assetRepository;
    }
    
    public async Task<List<GetAllDesktopQueryResult>> Handle(GetAllDesktopQuery request, CancellationToken cancellationToken)
    {
        var assets = await _assetRepository.GetAllByAssetTypeIdAsync("1", cancellationToken);  
        return assets.Select(x => new GetAllDesktopQueryResult
        {
            Id = x.Id,
            Name = x.Name,
            SerialNumber = x.SerialNumber,
            Brand = x.Brand,
            Model = x.Model,
            AssetTag = x.AssetTag,
            AssetTypeName = x.AssetType?.Name ?? "N/A",
            LocationName = x.Location?.Name ?? "N/A",
            AssignedToUserName = x.AssignedToUser?.FullName,
            PurchaseDate = x.PurchaseDate,
            WarrantyExpiryDate = x.WarrantyExpiryDate,
            Status = x.Status,
            Description = x.Description,
            DepartmentName = x.Department?.Name ?? "N/A",
        }).ToList();
    }
}