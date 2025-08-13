using MediatR;

namespace Application.Features.Commands.MaintenanceTypeCommands;

public class CreateMaintenanceTypeCommand : IRequest
{
    public string Name { get; set; }               // Örn: Haftalık Bakım Formu
    public string? Description { get; set; }
    public MaintenancePeriod Period { get; set; }  // Enum: Daily, Weekly, Monthly...
    public DateTime StartDate { get; set; }        // Başlangıç tarihi
    public DateTime? LastPerformedDate { get; set; }   // Opsiyonel: Son yapıldığı tarih
}