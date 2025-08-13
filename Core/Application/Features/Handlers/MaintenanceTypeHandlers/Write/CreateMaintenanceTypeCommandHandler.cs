using Application.Features.Commands.MaintenanceTypeCommands;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.MaintenanceTypeHandlers.Write;

public class CreateMaintenanceTypeCommandHandler : IRequestHandler<CreateMaintenanceTypeCommand>
{
    private readonly IGenericRepository<MaintenanceType> _repository;

    public CreateMaintenanceTypeCommandHandler(IGenericRepository<MaintenanceType> repository)
    {
         _repository = repository;
    }
    
    public async Task Handle(CreateMaintenanceTypeCommand request, CancellationToken cancellationToken)
    {
        var maintance = new MaintenanceType();
        maintance.Id = Guid.NewGuid().ToString();
        maintance.Name = request.Name;
        maintance.StartDate = request.StartDate;
        maintance.Period  = request.Period;
        await _repository.CreateAsync(maintance);
        await _repository.SaveChangesAsync();
    }
}