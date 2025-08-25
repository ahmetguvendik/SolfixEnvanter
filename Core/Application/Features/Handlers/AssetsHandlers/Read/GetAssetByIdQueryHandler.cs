using Application.Enums;
using Application.Features.Queries.AssetQueries;
using Application.Features.Results.AssetResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.AssetsHandlers.Read;

public class GetAssetByIdQueryHandler  : IRequestHandler<GetAssetByIdQuery, GetAssetByIdQueryResult>
{
    private readonly IAssetRepository _assetRepository;

    public GetAssetByIdQueryHandler(IAssetRepository assetRepository)
    {
         _assetRepository = assetRepository;
    }
    
    public async Task<GetAssetByIdQueryResult> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
    {
        var values = await _assetRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (values == null)
        {
            throw new ArgumentException($"Asset with ID {request.Id} not found.");
        }
        
        return new GetAssetByIdQueryResult
        {
            Name = values.Name,
            SerialNumber = values.SerialNumber,
            Brand = values.Brand,
            Model = values.Model,
            AssetTag = values.AssetTag,
            AssetTypeName = values.AssetType?.Name ?? "N/A",
            LocationName = values.Location?.Name ?? "N/A",
            AssignedToUserName = values.AssignedToUser?.FullName ?? "N/A",
            PurchaseDate = values.PurchaseDate,
            WarrantyExpiryDate = values.WarrantyExpiryDate,
            Status = values.Status,
            Description = values.Description,
            DepartmentName = values.Department?.Name ?? "N/A",
            CabinetName = values.Cabinet?.Name ?? "N/A",
        };
    }
}