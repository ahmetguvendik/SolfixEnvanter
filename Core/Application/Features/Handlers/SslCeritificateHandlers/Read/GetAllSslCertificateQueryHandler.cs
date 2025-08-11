using Application.Features.Queries.SslCertificateQueries;
using Application.Features.Results.SslCertificateResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.SslCeritificateHandlers.Read;

public class GetAllSslCertificateQueryHandler : IRequestHandler<GetAllSslCertificateQuery, IList<GetAllSslCertificateQueryResult>>
{
    private readonly ISslCertificateRepository _sslCertificateRepository;

    public GetAllSslCertificateQueryHandler(ISslCertificateRepository sslCertificateRepository)
    {
         _sslCertificateRepository = sslCertificateRepository;
    }
    
    public async Task<IList<GetAllSslCertificateQueryResult>> Handle(GetAllSslCertificateQuery request, CancellationToken cancellationToken)
    {
        var values = await _sslCertificateRepository.GetAllSslCertificatesWithDomain();
        return values.Select(x => new GetAllSslCertificateQueryResult
        {
            Id = x.Id,
            CommonName = x.CommonName,
            Provider = x.Provider,
            StartDate = x.StartDate,
            ExpirationDate = x.ExpirationDate,
            AutoRenew = x.AutoRenew,
            DomainName = x.Domain.Name,
            Notes = x.Notes,
        }).ToList();
    }
}