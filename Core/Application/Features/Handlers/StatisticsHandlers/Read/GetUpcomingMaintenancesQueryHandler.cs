using Application.Features.Queries.StatisticsQueries;
using Application.Features.Results.StatisticsResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.StatisticsHandlers.Read;

public class GetUpcomingMaintenancesQueryHandler : IRequestHandler<GetUpcomingMaintenancesQuery, List<GetUpcomingMaintenancesQueryResult>>
{
    private readonly IStatisticsRepository _statisticsRepository;

    public GetUpcomingMaintenancesQueryHandler(IStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }

    public async Task<List<GetUpcomingMaintenancesQueryResult>> Handle(GetUpcomingMaintenancesQuery request, CancellationToken cancellationToken)
    {
        var upcomingMaintenances = await _statisticsRepository.GetUpcomingMaintenancesAsync(request.Days, cancellationToken);
        
        return upcomingMaintenances.Select(m => new GetUpcomingMaintenancesQueryResult
        {
            Id = m.Id,
            MaintenanceTypeName = m.MaintenanceType.Name,
            ScheduledDate = m.ScheduledDate,
            CompletedByUserName = m.CompletedByUser?.UserName,
            Notes = m.Notes,
            Status = m.Status
        }).ToList();
    }
}
