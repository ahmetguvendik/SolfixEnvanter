using Application.Features.Queries.LocationQueries;
using Application.Features.Results.LocationsResults;
using Application.Interfaces;
using Domain.Entites;
using MediatR;

namespace Application.Features.Handlers.Locations.Read;
 
public class GetAllLocationQueryHandler  : IRequestHandler<GetAllLocationQuery, List<GetAllLocationQueryResult>>
{
    private readonly IGenericRepository<Location> _locationRepository;

    public GetAllLocationQueryHandler(IGenericRepository<Location> locationRepository)
    {
         _locationRepository = locationRepository;
    }
    public async Task<List<GetAllLocationQueryResult>> Handle(GetAllLocationQuery request, CancellationToken cancellationToken)
    {
        var  result = await _locationRepository.GetAllAsync();
        return result.Select(x=> new GetAllLocationQueryResult()
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();
    }
}