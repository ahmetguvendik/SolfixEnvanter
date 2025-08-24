using Application.Features.Commands.CabinetCommands;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.CabinetHandlers.Write;

public class CreateCabinetCommandHandler : IRequestHandler<CreateCabinetCommand>
{
    private readonly IGenericRepository<Domain.Entites.Cabinet> _cabinetRepository;

    public CreateCabinetCommandHandler(IGenericRepository<Domain.Entites.Cabinet> cabinetRepository)
    {
        _cabinetRepository = cabinetRepository;
    }
    
    public async Task Handle(CreateCabinetCommand request, CancellationToken cancellationToken)
    {
        var cabinet = new Domain.Entites.Cabinet();
        cabinet.Id = Guid.NewGuid().ToString();    
        cabinet.Name = request.Name;
        cabinet.Code = request.Code;
        cabinet.CoolingType = request.CoolingType;
        cabinet.LocationId = request.LocationId;
        cabinet.Manufacturer = request.Manufacturer;
        cabinet.MaxLoadKg = request.MaxLoadKg;
        cabinet.MaxPowerWatts = request.MaxPowerWatts;
        cabinet.Model = request.Model;
        cabinet.Notes = request.Notes;
        cabinet.PowerFeed = request.PowerFeed;
        cabinet.SerialNumber = request.SerialNumber;
        cabinet.UHeight = request.UHeight;
        await _cabinetRepository.CreateAsync(cabinet, cancellationToken);
        await _cabinetRepository.SaveChangesAsync(cancellationToken);

    }
}