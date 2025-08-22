using Application.Features.Results.AssignmentFormResults;
using MediatR;

namespace Application.Features.Queries.AssignmentFormQueries;

public class GetAllAssignmentFormQuery : IRequest<List<GetAllAssignmentFormQueryResult>>
{
}
