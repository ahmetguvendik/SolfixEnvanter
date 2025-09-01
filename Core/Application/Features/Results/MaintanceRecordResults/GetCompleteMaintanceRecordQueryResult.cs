using Domain.Enums;

namespace Application.Features.Results.MaintenanceRecordResults;

public class GetCompleteMaintenanceRecordQueryResult
{
    public string MaintenanceTypeId { get; set; }
    public string Id { get; set; }
    public string MaintenanceTypeName { get; set; } 
    public DateTime ScheduledDate { get; set; } // Planlanan bakım tarihi
    public DateTime? CompletedDate { get; set; } // Yapıldıysa tarihi
    public string? CompletedByUserName { get; set; } // Admin/Personel
    public string? Notes { get; set; } // Ek notlar
    public MaintenanceStatus Status { get; set; }
}