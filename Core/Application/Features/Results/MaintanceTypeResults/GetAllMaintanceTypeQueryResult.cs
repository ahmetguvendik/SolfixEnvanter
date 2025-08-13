namespace Application.Features.Results.MaintanceTypeResults;

public class GetAllMaintanceTypeQueryResult
{
    public string Id { get; set; }
    public string Name { get; set; }               // Örn: Haftalık Bakım Formu
    public MaintenancePeriod Period { get; set; }  // Enum: Daily, Weekly, Monthly...
    public DateTime StartDate { get; set; }        // Başlangıç tarihi
    public List<DateTime> UpcomingDates { get; set; }

}