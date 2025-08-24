using Application.Features.Queries.MaintenanceRecordQueries;
using Application.Features.Results.MaintenanceRecordResults;
using Application.Interfaces;
using Domain.Entites;
using MediatR;

namespace Application.Features.Handlers.MaintenanceRecordHandlers.Read;

public class GetCompleteMaintenanceRecordQueryHandler : IRequestHandler<GetCompleteMaintenanceRecordQuery, List<GetCompleteMaintenanceRecordQueryResult>>
{
    private readonly IMaintenanceRecordRepository _maintanceRecordRepository;

    public GetCompleteMaintenanceRecordQueryHandler(IMaintenanceRecordRepository maintanceRecordRepository)
    {
         _maintanceRecordRepository = maintanceRecordRepository;
    }
    
    public async Task<List<GetCompleteMaintenanceRecordQueryResult>> Handle(GetCompleteMaintenanceRecordQuery request, CancellationToken cancellationToken)
    {
        var date = (request.Date ?? DateTime.Today).Date;

        List<MaintenanceRecord> values;
        if (!string.IsNullOrWhiteSpace(request.CompletedByUserId))
        {
            values = await _maintanceRecordRepository.GetTodayCompletedByUser(request.CompletedByUserId, date);
        }
        else
        {
            values = await _maintanceRecordRepository.GetTodayRecordsWithUserAndType(date);
        }

        return values.Select(x => new GetCompleteMaintenanceRecordQueryResult
        {
            MaintenanceTypeId = x.MaintenanceTypeId,
            Id = x.Id,
            MaintenanceTypeName = x.MaintenanceType.Name,
            ScheduledDate = x.ScheduledDate,
            CompletedDate = x.CompletedDate,
            CompletedByUserName = x.CompletedByUser?.FullName,
            Notes = x.Notes,
            Status = x.Status
        }).ToList();
    }
}