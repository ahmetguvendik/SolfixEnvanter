using Application.Features.Results.SslCertificateResults;
using MediatR;

namespace Application.Features.Queries.SslCertificateQueries;

public class GetAllSslCertificateQuery : IRequest<IList<GetAllSslCertificateQueryResult>>
{
    
}