using Application.Enums;
using Application.Features.Queries.AssetQueries;
using Application.Features.Results.AssetResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.AssetsHandlers.Read;

public class GetAllPrinterQueryHandler : IRequestHandler<GetAllPrinterQuery, List<GetAllPrinterQueryResult>>
{
    private readonly IAssetRepository _assetRepository;

    public GetAllPrinterQueryHandler(IAssetRepository assetRepository)
    {
         _assetRepository = assetRepository;
    }
    
    public async Task<List<GetAllPrinterQueryResult>> Handle(GetAllPrinterQuery request, CancellationToken cancellationToken)
    {
        var results = await _assetRepository.GetAllByAssetTypeIdAsync("3", cancellationToken);
        return results.Select(x => new GetAllPrinterQueryResult
        {
            Id = x.Id,
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