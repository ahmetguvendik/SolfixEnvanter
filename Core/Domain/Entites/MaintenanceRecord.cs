using Domain.Entities;
using Domain.Enums;

namespace Domain.Entites;

public class MaintenanceRecord : BaseEntity
{
    public string MaintenanceTypeId { get; set; }
    public MaintenanceType MaintenanceType { get; set; }
    public DateTime ScheduledDate { get; set; } // Planlanan bakım tarihi
    public DateTime? CompletedDate { get; set; } // Yapıldıysa tarihi
    public string? CompletedByUserId { get; set; } // Admin/Personel
    public AppUser? CompletedByUser { get; set; }
    public string? Notes { get; set; } // Ek notlar
    public MaintenanceStatus Status { get; set; }
}