using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance.Contexts;

namespace Persistance.Repositories;

public class MaintanceRecordRepository : IMaintanceRecordRepository
{
    private readonly SolfixEnvanterDbContext  _context;

    public MaintanceRecordRepository(SolfixEnvanterDbContext context)
    {
         _context = context;
    }
    
    public async Task<List<MaintenanceRecord>> GetAllRecordsWithUserAndType()
    {
        var values = await _context.MaintenanceRecords.Include(x=>x.MaintenanceType).Include(y=>y.CompletedByUser).ToListAsync();
        return values;
    }
}