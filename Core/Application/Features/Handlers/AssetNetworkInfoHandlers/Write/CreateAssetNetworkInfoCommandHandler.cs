using Application.Features.Commands.AssetNetworkInfoCommands;
using Application.Interfaces;
using Domain.Entites;
using MediatR;

namespace Application.Features.Handlers.AssetNetworkInfoHandlers.Write;

public class CreateAssetNetworkInfoCommandHandler : IRequestHandler<CreateAssetNetworkInfoCommand>
{
    private readonly IGenericRepository<AssetNetworkInfo>  _assetNetworkInfoRepository;

    public CreateAssetNetworkInfoCommandHandler(IGenericRepository<AssetNetworkInfo> assetNetworkInfoRepository)
    {
         _assetNetworkInfoRepository = assetNetworkInfoRepository;
    }
    
    public async Task Handle(CreateAssetNetworkInfoCommand request, CancellationToken cancellationToken)
    {
        var assetNetworkInfo = new AssetNetworkInfo();
        assetNetworkInfo.Id = Guid.NewGuid().ToString();
        assetNetworkInfo.AssetId = request.AssetId;
        assetNetworkInfo.IPAddress  = request.IPAddress;
        assetNetworkInfo.MacAddress = request.MacAddress;
        await _assetNetworkInfoRepository.CreateAsync(assetNetworkInfo);
        await _assetNetworkInfoRepository.SaveChangesAsync();
    }
}