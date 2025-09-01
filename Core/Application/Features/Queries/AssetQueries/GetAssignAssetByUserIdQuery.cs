using Application.Features.Results.AssetResults;
using MediatR;

namespace Application.Features.Queries.AssetQueries;

public class GetAssignAssetByUserIdQuery : IRequest<List<GetAllAssignedUserAssetQueryResult>>
{
    public string UserId { get; set; }
}
