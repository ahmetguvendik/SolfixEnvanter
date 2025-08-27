using Application.Features.Results.AssetNetworkInfoResults;
using MediatR;

namespace Application.Features.Queries.AssetNetworkInfoQueries;

public class GetAssetNetworkInfoByIdQuery : IRequest<GetAssetNetworkInfoByIdQueryResult>
{
    public string Id { get; set; }
}
