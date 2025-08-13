using Application.Features.Queries.MaintanceRecordQueries;
using Application.Features.Results.MaintanceRecordResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.MaitanceRecordHandlers.Read;

public class GetCompleteMaintanceRecordQueryHandler : IRequestHandler<GetCompleteMaintanceRecordQuery, List<GetCompleteMaintanceRecordQueryResult>>
{
    private readonly IMaintanceRecordRepository _maintanceRecordRepository;

    public GetCompleteMaintanceRecordQueryHandler(IMaintanceRecordRepository maintanceRecordRepository)
    {
         _maintanceRecordRepository = maintanceRecordRepository;
    }
    
    public async Task<List<GetCompleteMaintanceRecordQueryResult>> Handle(GetCompleteMaintanceRecordQuery request, CancellationToken cancellationToken)
    {
       var values = await _maintanceRecordRepository.GetAllRecordsWithUserAndType();
       return values.Select(x => new GetCompleteMaintanceRecordQueryResult
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