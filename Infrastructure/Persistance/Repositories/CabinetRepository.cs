using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance.Contexts;

namespace Persistance.Repositories;

public class CabinetRepository  : ICabinetRepository
{
    private readonly SolfixEnvanterDbContext  _context;

    public CabinetRepository(SolfixEnvanterDbContext context)
    {
         _context = context;
    }
    
    public async Task<IList<Cabinet>> GetAllAsync()
    {
        var values = await _context.Cabinets.Include(x=>x.Location).Include(y=>y.Assets).ThenInclude(z=>z.AssetType).ToListAsync();
        return values;
    }
}