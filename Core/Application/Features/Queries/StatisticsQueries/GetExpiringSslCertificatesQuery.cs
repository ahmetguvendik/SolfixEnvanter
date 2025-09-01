using Application.Features.Results.StatisticsResults;
using MediatR;

namespace Application.Features.Queries.StatisticsQueries;

public class GetExpiringSslCertificatesQuery : IRequest<List<GetExpiringSslCertificatesQueryResult>>
{
    public int Days { get; set; } = 30;
}
