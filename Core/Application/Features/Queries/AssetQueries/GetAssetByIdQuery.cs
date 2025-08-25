using Application.Features.Results.AssetResults;
using MediatR;

namespace Application.Features.Queries.AssetQueries;

public class GetAssetByIdQuery : IRequest<GetAssetByIdQueryResult>
{
    public string Id { get; set; }

    public GetAssetByIdQuery(string id)
    {
         Id = id;
    }
    
}