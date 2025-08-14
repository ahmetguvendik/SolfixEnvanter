using Application.Features.Commands.DomainNameCommands;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.DomainNameHandlers.Write;

public class CreateDomainNameCommandHandler : IRequestHandler<CreateDomainNameCommand>
{
    private readonly IGenericRepository<DomainName> _repository;

    public CreateDomainNameCommandHandler(IGenericRepository<DomainName> repository)
    {
         _repository = repository;
    }
    
    public async Task Handle(CreateDomainNameCommand request, CancellationToken cancellationToken)
    {
        var domainName = new DomainName();
        domainName.Id = Guid.NewGuid().ToString();
        domainName.Name = request.Name;
        domainName.AutoRenew = request.AutoRenew;
        domainName.ExpirationDate  = request.ExpirationDate;
        domainName.HostingExpirationDate = request.HostingExpirationDate;
        domainName.HostingPlan  = request.HostingPlan;
        domainName.HostingProvider = request.HostingProvider;
        domainName.Notes =  request.Notes;
        domainName.Registrar = request.Registrar;
        domainName.RegistrationDate = request.RegistrationDate;
        domainName.ServerIP = request.ServerIP;
        await _repository.CreateAsync(domainName, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        
    }
}