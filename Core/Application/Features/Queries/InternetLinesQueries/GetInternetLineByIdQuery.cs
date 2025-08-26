using Application.Features.Results.InternetLinesResults;
using MediatR;

namespace Application.Features.Queries.InternetLinesQueries;

public class GetInternetLineByIdQuery : IRequest<GetInternetLineByIdQueryResult>
{
    public string Id { get; set; }
}
