using Application.Features.Results.AssetResults;
using MediatR;

namespace Application.Features.Queries.AssetQueries;

public class GetAllPrinterQuery : IRequest<List<GetAllPrinterQueryResult>>
{
    
}