using Application.Features.Results.DepartmentResults;
using MediatR;

namespace Application.Features.Queries.DepartmentQueries;

public class GetDepartmentQuery  : IRequest<List<GetDepartmentQueryResult>>
{
    
}