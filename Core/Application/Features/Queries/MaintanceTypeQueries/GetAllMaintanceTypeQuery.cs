using Application.Features.Results.MaintanceTypeResults;
using MediatR;

namespace Application.Features.Queries.MaintanceTypeQueries;

public class GetAllMaintanceTypeQuery : IRequest<List<GetAllMaintanceTypeQueryResult>>
{
    
}