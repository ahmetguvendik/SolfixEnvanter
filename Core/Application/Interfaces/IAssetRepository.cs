using Domain.Entities;

namespace Application.Interfaces;

public interface IAssetRepository
{
    Task<List<Asset>> GetAllByAssetTypeIdAsync(string assetTypeId);
    Task<List<Asset>> GetAllAssignedUser();
}