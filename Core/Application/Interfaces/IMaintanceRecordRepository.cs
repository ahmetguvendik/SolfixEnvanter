namespace Application.Interfaces;

public interface IMaintanceRecordRepository
{
    Task<List<MaintenanceRecord>> GetAllRecordsWithUserAndType();
}