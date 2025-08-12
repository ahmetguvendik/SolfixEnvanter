using Application.Features.Results.CabinetResults;
using MediatR;

namespace Application.Features.Queries.CabinetQueries;

public class GetAllCabinetQuery : IRequest<IList<GetAllCabinetQueryResult>>
{
    
}