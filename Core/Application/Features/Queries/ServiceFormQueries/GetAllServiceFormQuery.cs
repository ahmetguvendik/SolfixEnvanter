using Application.Features.Results.ServiceFormResults;
using MediatR;

namespace Application.Features.Queries.ServiceFormQueries;

public class GetAllServiceFormQuery : IRequest<List<GetAllServiceFormQueryResult>>
{
}
