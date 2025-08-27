using Application.Features.Results.CabinetResults;
using MediatR;

namespace Application.Features.Queries.CabinetQueries;

public class GetCabinetByIdQuery : IRequest<GetCabinetByIdQueryResult>
{
    public string Id { get; set; }
}
