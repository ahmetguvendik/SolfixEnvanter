using Application.Features.Results.LocationsResults;
using Domain.Entites;
using MediatR;

namespace Application.Features.Queries.LocationQueries;

public class GetAllLocationQuery : IRequest<List<GetAllLocationQueryResult>>
{
    
}