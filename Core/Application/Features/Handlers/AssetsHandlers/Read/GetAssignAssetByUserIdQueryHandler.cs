using Application.Features.Queries.AssetQueries;
using Application.Features.Results.AssetResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.AssetsHandlers.Read;

public class GetAssignAssetByUserIdQueryHandler : IRequestHandler<GetAssignAssetByUserIdQuery, List<GetAllAssignedUserAssetQueryResult>>
{
    private readonly IAssetRepository _assetRepository;

    public GetAssignAssetByUserIdQueryHandler(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }
    
    public async Task<List<GetAllAssignedUserAssetQueryResult>> Handle(GetAssignAssetByUserIdQuery request, CancellationToken cancellationToken)
    {
        var values = await _assetRepository.GetAssetsByUserId(request.UserId, cancellationToken);
        
        return values.Select(x => new GetAllAssignedUserAssetQueryResult()
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
