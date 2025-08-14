using Application.Interfaces;
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
}