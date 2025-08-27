using Application.Interfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CabinetRepository  : ICabinetRepository
{
    private readonly SolfixEnvanterDbContext  _context;

    public CabinetRepository(SolfixEnvanterDbContext context)
    {
         _context = context;
    }
    
    public async Task<IList<Cabinet>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var values = await _context.Cabinets
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x=>x.Location)
            .Include(y=>y.Assets)
            .ThenInclude(z=>z.AssetType)
            .ToListAsync(cancellationToken);
        return values;
    }
    
    public async Task<Cabinet> GetByIdWithDetailsAsync(string id, CancellationToken cancellationToken = default)
    {
        var cabinet = await _context.Cabinets
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Location)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return cabinet;
    }
}