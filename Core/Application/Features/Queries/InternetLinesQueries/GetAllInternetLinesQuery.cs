using Application.Features.Results.InternetLinesResults;
using MediatR;

namespace Application.Features.Queries.InternetLinesQueries;

public class GetAllInternetLinesQuery : IRequest<List<GetAllInternetLinesQueryResult>>
{
    
}