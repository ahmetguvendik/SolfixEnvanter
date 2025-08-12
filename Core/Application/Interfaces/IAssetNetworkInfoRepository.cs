using Domain.Entites;

namespace Application.Interfaces;

public interface IAssetNetworkInfoRepository
{
    Task<List<AssetNetworkInfo>> GetAllAssetNetworkInfoWithAssets();    
}