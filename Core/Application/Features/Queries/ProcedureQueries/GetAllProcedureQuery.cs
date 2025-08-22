using Application.Features.Results.ProcedureResults;
using MediatR;

namespace Application.Features.Queries.ProcedureQueries;

public class GetAllProcedureQuery : IRequest<List<GetAllProcedureQueryResult>>
{
}
