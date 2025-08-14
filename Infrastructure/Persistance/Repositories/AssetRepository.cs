using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

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

    public async Task<List<Asset>> GetAllAssignedUser()
    {
        return await _dbContext.Assets
            .Include(a => a.Location)
            .Include(a => a.Department)
            .Include(a => a.AssignedToUser)
            .Include(a => a.AssetType)
            .Where(a => a.AssignedToUserId != null)
            .ToListAsync();
    }
}