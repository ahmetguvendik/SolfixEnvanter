using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entites;
using Domain.Entities;
using MediatR;
using CabinetEntity = Domain.Entites.Cabinet;

namespace Application.Features.Handlers.Assets.Write;

public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand>
{
    private readonly IGenericRepository<Asset> _repository;
    private readonly IGenericRepository<CabinetEntity> _cabinetRepository;
    private readonly IGenericRepository<Location> _locationRepository;
    private readonly IGenericRepository<AssetType> _assetTypeRepository;
    private readonly IGenericRepository<Department> _departmentRepository;

    public UpdateAssetCommandHandler(
        IGenericRepository<Asset> repository, 
        IGenericRepository<CabinetEntity> cabinetRepository,
        IGenericRepository<Location> locationRepository,
        IGenericRepository<AssetType> assetTypeRepository,
        IGenericRepository<Department> departmentRepository)
    {
         _repository = repository;
         _cabinetRepository = cabinetRepository;
         _locationRepository = locationRepository;
         _assetTypeRepository = assetTypeRepository;
         _departmentRepository = departmentRepository;
    }
    
    public async Task Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (asset == null)
            throw new Exception("Asset not found");
            
        // Sadece null olmayan değerleri güncelle, null ise eski değeri koru
        if (!string.IsNullOrEmpty(request.Name))
            asset.Name = request.Name;
        if (!string.IsNullOrEmpty(request.SerialNumber))
            asset.SerialNumber = request.SerialNumber;
        if (!string.IsNullOrEmpty(request.Brand))
            asset.Brand = request.Brand;
        if (!string.IsNullOrEmpty(request.Model))
            asset.Model = request.Model;
        if (!string.IsNullOrEmpty(request.AssetTag))
            asset.AssetTag = request.AssetTag;
        if (!string.IsNullOrEmpty(request.Description))
            asset.Description = request.Description;
            
        // Foreign key validations - sadece değiştirilmek istenen alanlar için
        if (!string.IsNullOrEmpty(request.AssetTypeId) && request.AssetTypeId != "string" && request.AssetTypeId != "null")
        {
            var assetType = await _assetTypeRepository.GetByIdAsync(request.AssetTypeId, cancellationToken);
            if (assetType == null)
                throw new Exception($"AssetType with ID '{request.AssetTypeId}' not found");
            asset.AssetTypeId = request.AssetTypeId;
        }
        
        if (!string.IsNullOrEmpty(request.LocationId) && request.LocationId != "string" && request.LocationId != "null")
        {
            var location = await _locationRepository.GetByIdAsync(request.LocationId, cancellationToken);
            if (location == null)
                throw new Exception($"Location with ID '{request.LocationId}' not found");
            asset.LocationId = request.LocationId;
        }
        
        if (!string.IsNullOrEmpty(request.DepartmentId) && request.DepartmentId != "string" && request.DepartmentId != "null")
        {
            var department = await _departmentRepository.GetByIdAsync(request.DepartmentId, cancellationToken);
            if (department == null)
                throw new Exception($"Department with ID '{request.DepartmentId}' not found");
            asset.DepartmentId = request.DepartmentId;
        }
        
        // AssignedToUserId validation (optional) - null olabilir
        if (request.AssignedToUserId == "null" || string.IsNullOrEmpty(request.AssignedToUserId))
        {
            asset.AssignedToUserId = null; // User atamasını kaldır
        }
        else if (request.AssignedToUserId != "string")
        {
            asset.AssignedToUserId = request.AssignedToUserId;
        }
            
        if (request.PurchaseDate != default)
            asset.PurchaseDate = request.PurchaseDate;
            
        if (request.WarrantyExpiryDate.HasValue)
            asset.WarrantyExpiryDate = request.WarrantyExpiryDate;
            
        // Status her zaman güncelle
        if (request.Status.HasValue)
            asset.Status = request.Status.Value;
            
        // CabinetId validation (optional) - null olabilir
        if (request.CabinetId == "null" || string.IsNullOrEmpty(request.CabinetId))
        {
            asset.CabinetId = null; // Cabinet'ı kaldır
        }
        else if (request.CabinetId != "string")
        {
            var cabinet = await _cabinetRepository.GetByIdAsync(request.CabinetId, cancellationToken);
            if (cabinet == null)
                throw new Exception($"Cabinet with ID '{request.CabinetId}' not found");
            asset.CabinetId = request.CabinetId;
        }
        
        await _repository.UpdateAsync(asset, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}
