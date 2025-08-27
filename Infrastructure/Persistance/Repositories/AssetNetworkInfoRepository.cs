using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AssetNetworkInfoRepository : IAssetNetworkInfoRepository
{
    private readonly SolfixEnvanterDbContext  _dbContext;

    public AssetNetworkInfoRepository(SolfixEnvanterDbContext dbContext)
    {
         _dbContext = dbContext;
    }
    
    public async Task<List<AssetNetworkInfo>> GetAllAssetNetworkInfoWithAssets()
    {
        var values = await _dbContext.AssetNetworkInfos.Include(x => x.Asset).ToListAsync();
        return values;
    }
    
    public async Task<AssetNetworkInfo> GetAssetNetworkInfoWithAssetById(string id)
    {
        var assetNetworkInfo = await _dbContext.AssetNetworkInfos.Include(x => x.Asset).FirstOrDefaultAsync(x => x.Id == id);
        return assetNetworkInfo;
    }
}