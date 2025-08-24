using MediatR;

namespace Application.Features.Commands;

public class UpdateSslCertificateCommand : IRequest
{
    public string Id { get; set; }
    public string CommonName { get; set; }
    public string Provider { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool AutoRenew { get; set; }
    public string DomainId { get; set; }
    public string? Notes { get; set; }
}
