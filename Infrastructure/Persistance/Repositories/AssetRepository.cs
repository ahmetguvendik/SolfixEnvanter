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




    public async Task<List<Asset>> GetAllByAssetTypeIdAsync(string assetTypeId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Assets
            .AsNoTracking()
            .AsSplitQuery()
            .Include(a => a.Location)
            .Include(a => a.Department)
            .Include(a => a.AssignedToUser)
            .Include(a => a.AssetType)
            .Where(a => a.AssetTypeId == assetTypeId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Asset>> GetAllAssignedUser(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Assets
            .AsNoTracking()
            .AsSplitQuery()
            .Include(a => a.Location)
            .Include(a => a.Department)
            .Include(a => a.AssignedToUser)
            .Include(a => a.AssetType)
            .Where(a => a.AssignedToUserId != null)
            .ToListAsync(cancellationToken);
    }
}