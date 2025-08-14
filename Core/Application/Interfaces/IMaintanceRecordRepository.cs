namespace Application.Interfaces;

public interface IMaintenanceRecordRepository
{
    Task<List<MaintenanceRecord>> GetAllRecordsWithUserAndType();
    Task<List<MaintenanceRecord>> GetTodayRecordsWithUserAndType(DateTime date);
    Task<List<MaintenanceRecord>> GetTodayCompletedByUser(string userId, DateTime date);
}