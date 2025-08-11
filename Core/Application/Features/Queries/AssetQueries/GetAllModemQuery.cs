using Application.Features.Results.AssetResults;
using MediatR;

namespace Application.Features.Queries.AssetQueries;

public class GetAllModemQuery : IRequest<List<GetAllModemQueryResult>>
{
    
}