using Application.Interfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Persistance.Contexts;

namespace Persistance.Repositories;

public class AssetRepository  : IAssetRepository
{
    private readonly SolfixEnvanterDbContext  _dbContext;

    public AssetRepository(SolfixEnvanterDbContext dbContext)
    {
         _dbContext = dbContext;
    }




    public async Task<List<Asset>> GetAllByAssetTypeIdAsync(string assetTypeId)
    {
        return await _dbContext.Assets
            .Include(a => a.Location)
            .Include(a => a.Department)
            .Include(a => a.AssignedToUser)
            .Include(a => a.AssetType)
            .Where(a => a.AssetTypeId == assetTypeId)
            .ToListAsync();
    }
}