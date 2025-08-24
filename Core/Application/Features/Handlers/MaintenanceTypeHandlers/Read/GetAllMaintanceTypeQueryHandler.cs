using Application.Features.Queries.MaintenanceTypeQueries;
using Application.Features.Results.MaintenanceTypeResults;
using Application.Interfaces;
using Domain.Entites;
using MediatR;

namespace Application.Features.Handlers.MaintenanceTypeHandlers.Read;

public class GetAllMaintenanceTypeQueryHandler : IRequestHandler<GetAllMaintenanceTypeQuery, List<GetAllMaintenanceTypeQueryResult>>
{
    private readonly IGenericRepository<MaintenanceType>  _repository;
    private readonly IMaintenanceService _service;

    public GetAllMaintenanceTypeQueryHandler(IGenericRepository<MaintenanceType> repository, IMaintenanceService service)
    {
        _repository = repository;
        _service = service;
    }
    
    public async Task<List<GetAllMaintenanceTypeQueryResult>> Handle(GetAllMaintenanceTypeQuery request, CancellationToken cancellationToken)
    {
        var values = await _repository.GetAllAsync(cancellationToken);
        return values.Select(x => new GetAllMaintenanceTypeQueryResult
        {
            Id = x.Id,
            Name = x.Name,
            Period = x.Period,
            StartDate = x.StartDate,
            UpcomingDates = _service.GetUpcomingDates(x, 5) 
        }).ToList();
    }
}