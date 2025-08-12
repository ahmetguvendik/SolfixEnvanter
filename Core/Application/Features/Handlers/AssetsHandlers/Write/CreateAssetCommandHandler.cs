using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entites;
using MediatR;

namespace Application.Features.Handlers;

public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand>
{
    private readonly IGenericRepository<Asset>  _repository;

    public CreateAssetCommandHandler(IGenericRepository<Asset> repository)
    {
         _repository = repository;
    }
    
    public async Task Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        Asset asset = new Asset();
        asset.Id = Guid.NewGuid().ToString();
        asset.Name = request.Name;
        asset.AssetTag = request.AssetTag;
        asset.AssetTypeId = request.AssetTypeId;
        asset.AssignedToUserId = request.AssignedToUserId;
        asset.Brand = request.Brand;
        asset.LocationId = request.LocationId;
        asset.Model = request.Model;
        asset.WarrantyExpiryDate = request.WarrantyExpiryDate;
        asset.PurchaseDate = request.PurchaseDate;
        asset.Description = request.Description;
        asset.SerialNumber = request.SerialNumber;
        asset.Status = request.Status;
        asset.DepartmentId = request.DepartmentId;
        asset.CabinetId = request.CabinetId;
        await _repository.CreateAsync(asset);
        await _repository.SaveChangesAsync();   
    }
}