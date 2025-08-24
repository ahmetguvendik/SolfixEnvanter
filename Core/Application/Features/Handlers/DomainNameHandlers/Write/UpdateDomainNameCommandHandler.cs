using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entites;
using MediatR;

namespace Application.Features.Handlers.DomainNameHandlers.Write;

public class UpdateDomainNameCommandHandler : IRequestHandler<UpdateDomainNameCommand>
{
    private readonly IGenericRepository<DomainName> _repository;

    public UpdateDomainNameCommandHandler(IGenericRepository<DomainName> repository)
    {
         _repository = repository;
    }
    
    public async Task Handle(UpdateDomainNameCommand request, CancellationToken cancellationToken)
    {
        var domainName = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (domainName == null)
            throw new Exception("Domain Name not found");
            
        domainName.Name = request.Name;
        domainName.Registrar = request.Registrar;
        domainName.RegistrationDate = request.RegistrationDate;
        domainName.ExpirationDate = request.ExpirationDate;
        domainName.AutoRenew = request.AutoRenew;
        domainName.HostingProvider = request.HostingProvider;
        domainName.HostingPlan = request.HostingPlan;
        domainName.ServerIP = request.ServerIP;
        domainName.HostingExpirationDate = request.HostingExpirationDate;
        domainName.Notes = request.Notes;
        
        await _repository.UpdateAsync(domainName, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}
