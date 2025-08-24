using Application.Interfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class MaintenanceRecordRepository : IMaintenanceRecordRepository
{
    private readonly SolfixEnvanterDbContext  _context;

    public MaintenanceRecordRepository(SolfixEnvanterDbContext context)
    {
         _context = context;
    }
    
    public async Task<List<MaintenanceRecord>> GetAllRecordsWithUserAndType()
    {
        var values = await _context.MaintenanceRecords.Include(x=>x.MaintenanceType).Include(y=>y.CompletedByUser).ToListAsync();
        return values;
    }

    public async Task<List<MaintenanceRecord>> GetTodayRecordsWithUserAndType(DateTime date)
    {
        var day = date.Date;
        return await _context.MaintenanceRecords
            .Include(x => x.MaintenanceType)
            .Include(y => y.CompletedByUser)
            .Where(r => r.ScheduledDate.Date == day)
            .ToListAsync();
    }

    public async Task<List<MaintenanceRecord>> GetTodayCompletedByUser(string userId, DateTime date)
    {
        var day = date.Date;
        return await _context.MaintenanceRecords
            .Include(x => x.MaintenanceType)
            .Include(y => y.CompletedByUser)
            .Where(r => r.ScheduledDate.Date == day && r.CompletedByUserId == userId)
            .ToListAsync();
    }
}