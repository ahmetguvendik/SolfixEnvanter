using Application.DTOs;
using Application.Features.Queries.CabinetQueries;
using Application.Features.Results.CabinetResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.CabinetHandlers.Read;

public class GetAllCabinetQueryHandler : IRequestHandler<GetAllCabinetQuery, IList<GetAllCabinetQueryResult>>
{
    private readonly ICabinetRepository _cabinetRepository;

    public GetAllCabinetQueryHandler(ICabinetRepository cabinetRepository)
    {
         _cabinetRepository = cabinetRepository;
    }
    
    public async Task<IList<GetAllCabinetQueryResult>> Handle(GetAllCabinetQuery request, CancellationToken cancellationToken)
    {
        var  cabinets = await _cabinetRepository.GetAllAsync();
        return cabinets.Select(x => new GetAllCabinetQueryResult
        {
            Id = x.Id,
            Name = x.Name,
            Code = x.Code,
            UHeight = x.UHeight,
            Manufacturer = x.Manufacturer,
            Model = x.Model,
            SerialNumber = x.SerialNumber,
            PowerFeed = x.PowerFeed,
            MaxLoadKg = x.MaxLoadKg,
            MaxPowerWatts = x.MaxPowerWatts,
            CoolingType = x.CoolingType,
            Notes = x.Notes,
            LocationName = x.Location.Name,
            Assets = x.Assets.Select(a => new AssetInCabinetDto
            {
                Id = a.Id,
                Name = a.Name,
                SerialNumber = a.SerialNumber,
                Brand = a.Brand,
                Model = a.Model,
                AssetTag = a.AssetTag,
                AssetStatus = a.Status,
                AssetTypeName = a.AssetType.Name,
            }).ToList()
        }).ToList();
    }
}