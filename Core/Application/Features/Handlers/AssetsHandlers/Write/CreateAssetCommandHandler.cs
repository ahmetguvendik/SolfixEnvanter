using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Handlers;

public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand>
{
    private readonly IGenericRepository<Asset>  _repository;
    private readonly ILogger<CreateAssetCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateAssetCommandHandler(IGenericRepository<Asset> repository, ILogger<CreateAssetCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
    {
         _repository = repository;
         _logger = logger;
         _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        _logger.LogInformation("User {UserId} is creating asset {Name} ({AssetTag})", userId, request.Name, request.AssetTag);

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
        await _repository.CreateAsync(asset, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("User {UserId} created asset {Id}", userId, asset.Id);
    }
}