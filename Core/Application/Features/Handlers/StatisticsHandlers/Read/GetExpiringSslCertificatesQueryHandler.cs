using Application.Features.Queries.StatisticsQueries;
using Application.Features.Results.StatisticsResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.StatisticsHandlers.Read;

public class GetExpiringSslCertificatesQueryHandler : IRequestHandler<GetExpiringSslCertificatesQuery, List<GetExpiringSslCertificatesQueryResult>>
{
    private readonly IStatisticsRepository _statisticsRepository;

    public GetExpiringSslCertificatesQueryHandler(IStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }

    public async Task<List<GetExpiringSslCertificatesQueryResult>> Handle(GetExpiringSslCertificatesQuery request, CancellationToken cancellationToken)
    {
        var expiringSslCertificates = await _statisticsRepository.GetExpiringSslCertificatesAsync(request.Days, cancellationToken);
        var today = DateTime.Today;
        
        return expiringSslCertificates.Select(s => new GetExpiringSslCertificatesQueryResult
        {
            Id = s.Id,
            CommonName = s.CommonName,
            Provider = s.Provider,
            StartDate = s.StartDate,
            ExpirationDate = s.ExpirationDate,
            AutoRenew = s.AutoRenew,
            DomainName = s.Domain.Name,
            Notes = s.Notes,
            DaysUntilExpiration = (s.ExpirationDate - today).Days
        }).ToList();
    }
}
