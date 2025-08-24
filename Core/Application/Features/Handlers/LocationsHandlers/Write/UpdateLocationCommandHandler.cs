using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Handlers.Locations.Write;

public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand>
{
    private readonly IGenericRepository<Location> _repository;

    public UpdateLocationCommandHandler(IGenericRepository<Location> repository)
    {
         _repository = repository;
    }
    
    public async Task Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (location == null)
            throw new Exception("Location not found");
            
        location.Name = request.Name;
        await _repository.UpdateAsync(location, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}
