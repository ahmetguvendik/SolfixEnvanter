using Application.Features.Queries.CabinetQueries;
using Application.Features.Results.CabinetResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.CabinetHandlers.Read;

public class GetCabinetByIdQueryHandler : IRequestHandler<GetCabinetByIdQuery, GetCabinetByIdQueryResult>
{
    private readonly ICabinetRepository _cabinetRepository;

    public GetCabinetByIdQueryHandler(ICabinetRepository cabinetRepository)
    {
        _cabinetRepository = cabinetRepository;
    }
    
    public async Task<GetCabinetByIdQueryResult> Handle(GetCabinetByIdQuery request, CancellationToken cancellationToken)
    {
        var cabinet = await _cabinetRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);
        
        if (cabinet == null)
            throw new Exception("Cabinet not found");
            
        return new GetCabinetByIdQueryResult
        {
            Id = cabinet.Id,
            Name = cabinet.Name,
            Code = cabinet.Code,
            UHeight = cabinet.UHeight,
            Manufacturer = cabinet.Manufacturer,
            Model = cabinet.Model,
            SerialNumber = cabinet.SerialNumber,
            PowerFeed = cabinet.PowerFeed,
            MaxLoadKg = cabinet.MaxLoadKg,
            MaxPowerWatts = cabinet.MaxPowerWatts,
            CoolingType = cabinet.CoolingType,
            Notes = cabinet.Notes,
            LocationName = cabinet.Location.Name
        };
    }
}
