
using Domain.Enums;

namespace Application.Features.Results.StatisticsResults;

public class GetUpcomingMaintenancesQueryResult
{
    public string Id { get; set; }
    public string MaintenanceTypeName { get; set; }
    public DateTime ScheduledDate { get; set; }
    public string? CompletedByUserName { get; set; }
    public string? Notes { get; set; }
    public MaintenanceStatus Status { get; set; }
}
