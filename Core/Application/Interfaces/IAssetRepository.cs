using Domain.Entities;

namespace Application.Interfaces;

public interface IAssetRepository
{
    Task<List<Asset>> GetAllByAssetTypeIdAsync(string assetTypeId, CancellationToken cancellationToken = default);
    Task<List<Asset>> GetAllAssignedUser(CancellationToken cancellationToken = default);
    Task<List<Asset>> GetAssetsByUserId(string userId, CancellationToken cancellationToken = default);
    Task<Asset> GetByIdAsync(string id, CancellationToken cancellationToken = default);
}