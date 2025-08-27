using Application.Features.Commands.AssetNetworkInfoCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Handlers.AssetNetworkInfoHandlers.Write;

public class CreateAssetNetworkInfoCommandHandler : IRequestHandler<CreateAssetNetworkInfoCommand>
{
    private readonly IGenericRepository<Domain.Entities.AssetNetworkInfo>  _assetNetworkInfoRepository;

    public CreateAssetNetworkInfoCommandHandler(IGenericRepository<Domain.Entities.AssetNetworkInfo> assetNetworkInfoRepository)
    {
         _assetNetworkInfoRepository = assetNetworkInfoRepository;
    }
    
    public async Task Handle(CreateAssetNetworkInfoCommand request, CancellationToken cancellationToken)
    {
        var assetNetworkInfo = new Domain.Entities.AssetNetworkInfo();
        assetNetworkInfo.Id = Guid.NewGuid().ToString();
        assetNetworkInfo.AssetId = request.AssetId;
        assetNetworkInfo.IPAddress  = request.IPAddress;
        assetNetworkInfo.MacAddress = request.MacAddress;
        assetNetworkInfo.Status = request.Status;
        await _assetNetworkInfoRepository.CreateAsync(assetNetworkInfo, cancellationToken);
        await _assetNetworkInfoRepository.SaveChangesAsync(cancellationToken);
    }
}