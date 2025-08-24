using Application.Features.Queries.AssetQueries;
using Application.Features.Results.AssetResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.AssetsHandlers.Read;

public class GetAllSystemSoftwareQueryHandler  : IRequestHandler<GetAllSystemSoftwareQuery, IList<GetAllSystemSoftwareQueryResult>>
{
    private readonly IAssetRepository _repository;

    public GetAllSystemSoftwareQueryHandler(IAssetRepository repository)
    {
         _repository = repository;
    }
    
    public async Task<IList<GetAllSystemSoftwareQueryResult>> Handle(GetAllSystemSoftwareQuery request, CancellationToken cancellationToken)
    {
        var values = await _repository.GetAllByAssetTypeIdAsync("12", cancellationToken);
        return values.Select(x=> new GetAllSystemSoftwareQueryResult()
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