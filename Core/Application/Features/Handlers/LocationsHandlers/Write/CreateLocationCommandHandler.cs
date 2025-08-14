using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Handlers.Locations.Write;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand>
{
    private readonly IGenericRepository<Location> _repository;

    public CreateLocationCommandHandler(IGenericRepository<Location> repository)
    {
         _repository = repository;
    }
    
    public async Task Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        Location location = new Location();
        location.Id = Guid.NewGuid().ToString();
        location.Name = request.Name;
        await _repository.CreateAsync(location);
        await _repository.SaveChangesAsync();
    }
}