using Domain.Entities;

namespace Application.Interfaces;

public interface IAssetNetworkInfoRepository
{
    Task<List<AssetNetworkInfo>> GetAllAssetNetworkInfoWithAssets();
    Task<AssetNetworkInfo> GetAssetNetworkInfoWithAssetById(string id);
}