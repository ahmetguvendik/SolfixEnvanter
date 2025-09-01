using Application.Features.Queries.StatisticsQueries;
using Application.Features.Results.StatisticsResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.StatisticsHandlers.Read;

public class GetExpiringDomainsQueryHandler : IRequestHandler<GetExpiringDomainsQuery, List<GetExpiringDomainsQueryResult>>
{
    private readonly IStatisticsRepository _statisticsRepository;

    public GetExpiringDomainsQueryHandler(IStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }

    public async Task<List<GetExpiringDomainsQueryResult>> Handle(GetExpiringDomainsQuery request, CancellationToken cancellationToken)
    {
        var expiringDomains = await _statisticsRepository.GetExpiringDomainsAsync(request.Days, cancellationToken);
        var today = DateTime.Today;
        
        return expiringDomains.Select(d => new GetExpiringDomainsQueryResult
        {
            Id = d.Id,
            Name = d.Name,
            Registrar = d.Registrar,
            RegistrationDate = d.RegistrationDate,
            ExpirationDate = d.ExpirationDate,
            AutoRenew = d.AutoRenew,
            HostingProvider = d.HostingProvider,
            Notes = d.Notes,
            DaysUntilExpiration = (d.ExpirationDate - today).Days
        }).ToList();
    }
}
