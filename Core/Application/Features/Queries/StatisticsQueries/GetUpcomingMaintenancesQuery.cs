using Application.Features.Results.StatisticsResults;
using MediatR;

namespace Application.Features.Queries.StatisticsQueries;

public class GetUpcomingMaintenancesQuery : IRequest<List<GetUpcomingMaintenancesQueryResult>>
{
    public int Days { get; set; } = 30;
}
