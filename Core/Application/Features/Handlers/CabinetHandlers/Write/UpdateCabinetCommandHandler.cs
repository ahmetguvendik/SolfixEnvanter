using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entites;
using MediatR;

namespace Application.Features.Handlers.Cabinet.Write;

public class UpdateCabinetCommandHandler : IRequestHandler<UpdateCabinetCommand>
{
    private readonly IGenericRepository<Domain.Entites.Cabinet> _repository;

    public UpdateCabinetCommandHandler(IGenericRepository<Domain.Entites.Cabinet> repository)
    {
         _repository = repository;
    }
    
    public async Task Handle(UpdateCabinetCommand request, CancellationToken cancellationToken)
    {
        var cabinet = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (cabinet == null)
            throw new Exception("Cabinet not found");
            
        cabinet.Name = request.Name;
        cabinet.Code = request.Code;
        cabinet.UHeight = request.UHeight;
        cabinet.Manufacturer = request.Manufacturer;
        cabinet.Model = request.Model;
        cabinet.SerialNumber = request.SerialNumber;
        cabinet.PowerFeed = request.PowerFeed;
        cabinet.MaxLoadKg = request.MaxLoadKg;
        cabinet.MaxPowerWatts = request.MaxPowerWatts;
        cabinet.CoolingType = request.CoolingType;
        cabinet.Notes = request.Notes;
        cabinet.LocationId = request.LocationId;
        
        await _repository.UpdateAsync(cabinet, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}
