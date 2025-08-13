using MediatR;

namespace Application.Features.Commands.MaintenanceRecordCommands
{
    public class CompleteMaintenanceCommand : IRequest
    {
        public string MaintenanceRecordId { get; set; } // Güncellenecek bakım kaydının ID'si
        public string CompletedByUserId { get; set; }   // Butona basan kullanıcı
        public string? Notes { get; set; }              // Opsiyonel not
    }
}