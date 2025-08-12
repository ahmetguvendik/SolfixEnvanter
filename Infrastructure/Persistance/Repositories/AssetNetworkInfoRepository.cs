using Application.Interfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Persistance.Contexts;

namespace Persistance.Repositories;

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
}