using Application.Features.Commands.MaintenanceRecordCommands;
using Application.Interfaces;
using Domain.Entites;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Handlers.MaintenanceRecordHandlers.Write
{
    public class CompleteMaintenanceByTypeCommandHandler : IRequestHandler<CompleteMaintenanceCommand>
    {
        private readonly IGenericRepository<MaintenanceType> _typeRepo;
        private readonly IGenericRepository<MaintenanceRecord> _recordRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompleteMaintenanceByTypeCommandHandler(
            IGenericRepository<MaintenanceType> typeRepo,
            IGenericRepository<MaintenanceRecord> recordRepo,
            IHttpContextAccessor httpContextAccessor)
        {
            _typeRepo = typeRepo;
            _recordRepo = recordRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Handle(CompleteMaintenanceCommand request, CancellationToken cancellationToken)
        {
            // 1) Type kontrolü
            var type = await _typeRepo.GetByIdAsync(request.maintenanceTypeId, cancellationToken);
            if (type == null)
            {
                throw new Exception("Bakım tipi bulunamadı.");
            }

            var today = DateTime.Today;

            // 2) Bugünün kaydını bul (repo predicate yoksa GetAllAsync ile filtreleyebilirsin)
            // İdeal: FirstOrDefaultAsync(predicate) gibi bir metodun olsun.
            var allRecords = await _recordRepo.GetAllAsync(cancellationToken); // Geçici çözüm, verimli değil
            var record = allRecords
                .FirstOrDefault(r => r.MaintenanceTypeId == request.maintenanceTypeId
                                  && r.ScheduledDate.Date == today);

            // 3) Yoksa oluştur
            if (record == null)
            {
                record = new MaintenanceRecord
                {
                    Id = Guid.NewGuid().ToString(),
                    MaintenanceTypeId = request.maintenanceTypeId,
                    ScheduledDate = today,
                    Status = MaintenanceStatus.Scheduled
                };
                await _recordRepo.CreateAsync(record, cancellationToken);
            }

            // 4) Gelecek tarih güvenliği (UI zaten göstermiyorsa yine de kalsın)
            if (record.ScheduledDate.Date > today)
                throw new Exception("Gelecek tarihli bakım tamamlanamaz.");

            // 5) Tamamla / Kaçırıldı
            record.CompletedByUserId = request.CompletedByUserId;
            record.CompletedDate = DateTime.Now;
            record.Notes = request.Notes;
            record.Status = record.ScheduledDate.Date < today
                ? MaintenanceStatus.Missed
                : MaintenanceStatus.Completed;

            await _recordRepo.SaveChangesAsync(cancellationToken);
        }
    }
}
