using MediatR;

namespace Application.Features.Commands.SslCertificateCommands;

public class CreateSslCertificateCommand : IRequest
{    
    public string CommonName { get; set; } // Alan adı veya wildcard (*.example.com)
    public string Provider { get; set; } // Let's Encrypt, Sectigo vb.
    public DateTime StartDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool AutoRenew { get; set; }
    public string DomainId { get; set; }
    public string? Notes { get; set; }
    
}