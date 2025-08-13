using Application.Features.Queries.MaintanceTypeQueries;
using Application.Features.Results.MaintanceTypeResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.MaintenanceTypeHandlers.Read;

public class GetAllMaintanceTypeQueryHandler : IRequestHandler<GetAllMaintanceTypeQuery, List<GetAllMaintanceTypeQueryResult>>
{
    private readonly IGenericRepository<MaintenanceType>  _repository;
    private readonly IMaintenanceService _service;

    public GetAllMaintanceTypeQueryHandler(IGenericRepository<MaintenanceType> repository, IMaintenanceService service)
    {
        _repository = repository;
        _service = service;
    }
    
    public async Task<List<GetAllMaintanceTypeQueryResult>> Handle(GetAllMaintanceTypeQuery request, CancellationToken cancellationToken)
    {
        var values = await _repository.GetAllAsync();
        return values.Select(x => new GetAllMaintanceTypeQueryResult
        {
            Id = x.Id,
            Name = x.Name,
            Period = x.Period,
            StartDate = x.StartDate,
            UpcomingDates = _service.GetUpcomingDates(x, 5) 
        }).ToList();
    }
}