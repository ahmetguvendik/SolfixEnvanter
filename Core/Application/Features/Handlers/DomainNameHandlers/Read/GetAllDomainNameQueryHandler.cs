using Application.Features.Queries.DomainNameQueries;
using Application.Features.Results.DomainNameResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.DomainNameHandlers.Read;

public class GetAllDomainNameQueryHandler  : IRequestHandler<GetAllDomainNameQuery, List<GetAllDomainNameQueryResult>>
{
    private readonly IGenericRepository<DomainName> _repository;

    public GetAllDomainNameQueryHandler(IGenericRepository<DomainName> repository)
    {
         _repository = repository;
    }
    
    public async Task<List<GetAllDomainNameQueryResult>> Handle(GetAllDomainNameQuery request, CancellationToken cancellationToken)
    {
        var values =  await _repository.GetAllAsync();
        return values.Select(x => new GetAllDomainNameQueryResult
        {
            Id = x.Id,
            Name = x.Name,
            Registrar = x.Registrar,
            RegistrationDate = x.RegistrationDate,
            ExpirationDate = x.ExpirationDate,
            AutoRenew = x.AutoRenew,
            HostingProvider = x.HostingProvider,
            HostingPlan = x.HostingPlan,
            ServerIP = x.ServerIP,
            HostingExpirationDate = x.HostingExpirationDate,
            Notes = x.Notes
        }).ToList();
    }
}