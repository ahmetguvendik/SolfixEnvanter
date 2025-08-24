using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entites;
using MediatR;

namespace Application.Features.Handlers.AssetNetworkInfo.Write;

public class UpdateAssetNetworkInfoCommandHandler : IRequestHandler<UpdateAssetNetworkInfoCommand>
{
    private readonly IGenericRepository<Domain.Entities.AssetNetworkInfo> _repository;

    public UpdateAssetNetworkInfoCommandHandler(IGenericRepository<Domain.Entities.AssetNetworkInfo> repository)
    {
         _repository = repository;
    }
    
    public async Task Handle(UpdateAssetNetworkInfoCommand request, CancellationToken cancellationToken)
    {
        var networkInfo = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (networkInfo == null)
            throw new Exception("Asset Network Info not found");
            
        networkInfo.AssetId = request.AssetId;
        networkInfo.IPAddress = request.IPAddress;
        networkInfo.MacAddress = request.MacAddress;
        
        await _repository.UpdateAsync(networkInfo, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}
