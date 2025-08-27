using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Handlers.Assets.Write;

public class AssignAssetToUserCommandHandler : IRequestHandler<AssignAssetToUserCommand>
{
    private readonly IGenericRepository<Asset> _assetRepository;
    private readonly IGenericRepository<AppUser> _userRepository;

    public AssignAssetToUserCommandHandler(
        IGenericRepository<Asset> assetRepository,
        IGenericRepository<AppUser> userRepository)
    {
        _assetRepository = assetRepository;
        _userRepository = userRepository;
    }
    
    public async Task Handle(AssignAssetToUserCommand request, CancellationToken cancellationToken)
    {
        // Asset'i bul
        var asset = await _assetRepository.GetByIdAsync(request.AssetId, cancellationToken);
        if (asset == null)
            throw new Exception("Asset not found");
            
        // Eğer UserId null ise, zimmeti kaldır
        if (string.IsNullOrEmpty(request.UserId))
        {
            asset.AssignedToUserId = null;
        }
        else
        {
            // User'ı kontrol et
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                throw new Exception("User not found");
                
            asset.AssignedToUserId = request.UserId;
        }
        
        await _assetRepository.UpdateAsync(asset, cancellationToken);
        await _assetRepository.SaveChangesAsync(cancellationToken);
    }
}
