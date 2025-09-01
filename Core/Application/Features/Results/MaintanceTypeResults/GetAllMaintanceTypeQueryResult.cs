using Domain.Enums;

namespace Application.Features.Results.MaintenanceTypeResults;

public class GetAllMaintenanceTypeQueryResult
{
    public string Id { get; set; }
    public string Name { get; set; }               // Örn: Haftalık Bakım Formu
    public MaintenancePeriod Period { get; set; }  // Enum: Daily, Weekly, Monthly...
    public DateTime StartDate { get; set; }        // Başlangıç tarihi
    public List<DateTime> UpcomingDates { get; set; }

}