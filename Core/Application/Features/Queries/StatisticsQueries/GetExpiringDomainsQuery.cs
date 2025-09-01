using Application.Features.Results.StatisticsResults;
using MediatR;

namespace Application.Features.Queries.StatisticsQueries;

public class GetExpiringDomainsQuery : IRequest<List<GetExpiringDomainsQueryResult>>
{
    public int Days { get; set; } = 30;
}
