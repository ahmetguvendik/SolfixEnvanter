using Application.Features.Commands.MaintenanceRecordCommands;
using Application.Interfaces;
using Domain.Entites;
using MediatR;

namespace Application.Features.Handlers.MaintenanceRecordHandlers.Write
{
    public class CompleteMaintenanceCommandHandler : IRequestHandler<CompleteMaintenanceCommand>
    {
        private readonly IGenericRepository<MaintenanceRecord> _repository;

        public CompleteMaintenanceCommandHandler(IGenericRepository<MaintenanceRecord> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CompleteMaintenanceCommand request, CancellationToken cancellationToken)
        {
            // Önce bakım kaydını al
            var record = await _repository.GetByIdAsync(request.MaintenanceRecordId);
            if (record == null)
                throw new Exception("Bakım kaydı bulunamadı.");

            // Tamamlayan kullanıcı ve notları ata
            record.CompletedByUserId = request.CompletedByUserId;
            record.CompletedDate = DateTime.Now; // veya request.CompletedDate kullanabilirsin
            record.Notes = request.Notes;

            // Status kontrolü: geçmişse Missed, değilse Completed
            if (record.ScheduledDate < DateTime.Today)
                record.Status = MaintenanceStatus.Missed;
            else
                record.Status = MaintenanceStatus.Completed;

            await _repository.UpdateAsync(record);
            await _repository.SaveChangesAsync();
        }
    }
}