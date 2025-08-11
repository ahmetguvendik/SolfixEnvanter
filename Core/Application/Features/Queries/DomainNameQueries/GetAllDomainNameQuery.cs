using Application.Features.Results.DomainNameResults;
using MediatR;

namespace Application.Features.Queries.DomainNameQueries;

public class GetAllDomainNameQuery : IRequest<List<GetAllDomainNameQueryResult>>
{
    
}