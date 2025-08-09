using Application.Features.Results;
using MediatR;

namespace Application.Features.Queries.AppUserQueries;

public class GetUserQuery : IRequest<List<GetUserQueryResult>>
{
    
}