using Application.Interfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class InternetLinesRepository : IInternetLinesRepository
{
    private readonly SolfixEnvanterDbContext  _context;

    public InternetLinesRepository(SolfixEnvanterDbContext context)
    {
         _context = context;
    }
    
    public async Task<List<InternetLine>> GetAllInternetLinesWithLocation()
    {
        var values = await _context.InternetLines.Include(x => x.Location).ToListAsync();
        return values;
    }
}