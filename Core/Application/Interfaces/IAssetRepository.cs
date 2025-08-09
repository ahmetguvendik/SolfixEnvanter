using Domain.Entites;

namespace Application.Interfaces;

public interface IAssetRepository
{
    Task<List<Asset>> GetAllByAssetTypeIdAsync(string assetTypeId);
}