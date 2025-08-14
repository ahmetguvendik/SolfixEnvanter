using Application.Features.Results.MaintenanceRecordResults;
using MediatR;

namespace Application.Features.Queries.MaintenanceRecordQueries;

public class GetCompleteMaintenanceRecordQuery : IRequest<List<GetCompleteMaintenanceRecordQueryResult>>
{
    public DateTime? Date { get; set; }
    public string? CompletedByUserId { get; set; }
}