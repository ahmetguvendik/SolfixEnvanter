using Application.Features.Queries.AssetQueries;
using Application.Features.Results.AssetResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.AssetsHandlers.Read;

public class GetAllLaptopQueryHandler : IRequestHandler<GetAllLaptopQuery, List<GetAllLaptopQueryResult>>
{
    private readonly IAssetRepository _assetRepository;

    public GetAllLaptopQueryHandler(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }
    
    
    public async Task<List<GetAllLaptopQueryResult>> Handle(GetAllLaptopQuery request, CancellationToken cancellationToken)
    {
        var assets = await _assetRepository.GetAllByAssetTypeIdAsync("2", cancellationToken);  
        return assets.Select(x => new GetAllLaptopQueryResult
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