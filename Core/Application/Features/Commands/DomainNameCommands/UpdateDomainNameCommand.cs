using MediatR;

namespace Application.Features.Commands;

public class UpdateDomainNameCommand : IRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Registrar { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool AutoRenew { get; set; }
    public string? HostingProvider { get; set; }
    public string? HostingPlan { get; set; }
    public string? ServerIP { get; set; }
    public DateTime? HostingExpirationDate { get; set; }
    public string? Notes { get; set; }
}
