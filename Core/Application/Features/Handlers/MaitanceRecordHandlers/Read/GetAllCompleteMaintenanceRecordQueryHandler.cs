using Application.Features.Queries.MaintenanceRecordQueries;
using Application.Features.Results.MaintanceRecordResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.MaintenanceRecordHandlers.Read;

public class GetAllCompleteMaintenanceRecordQueryHandler : IRequestHandler<GetAllCompleteMaintenanceRecordQuery, List<GetAllCompleteMaintenanceRecordQueryResult>>
{
    private readonly IMaintenanceRecordRepository  _repository;

    public GetAllCompleteMaintenanceRecordQueryHandler(IMaintenanceRecordRepository  repository)
    {
         _repository = repository;
    }
    
    public async Task<List<GetAllCompleteMaintenanceRecordQueryResult>> Handle(GetAllCompleteMaintenanceRecordQuery request, CancellationToken cancellationToken)
    {
        var values = await _repository.GetAllRecordsWithUserAndType();
        return values.Select(x => new GetAllCompleteMaintenanceRecordQueryResult
        {
            Id = x.Id,
            MaintenanceTypeName = x.MaintenanceType.Name,
            ScheduledDate = x.ScheduledDate,
            CompletedDate = x.CompletedDate,
            CompletedByUserName = x.CompletedByUser.FullName,
            Notes = x.Notes,
            Status = x.Status
        }).ToList();
    }
}