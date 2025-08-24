using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entites;
using MediatR;

namespace Application.Features.Handlers.SslCeritificateHandlers.Write;

public class UpdateSslCertificateCommandHandler : IRequestHandler<UpdateSslCertificateCommand>
{
    private readonly IGenericRepository<SslCertificate> _repository;

    public UpdateSslCertificateCommandHandler(IGenericRepository<SslCertificate> repository)
    {
         _repository = repository;
    }
    
    public async Task Handle(UpdateSslCertificateCommand request, CancellationToken cancellationToken)
    {
        var sslCertificate = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (sslCertificate == null)
            throw new Exception("SSL Certificate not found");
            
        sslCertificate.CommonName = request.CommonName;
        sslCertificate.Provider = request.Provider;
        sslCertificate.StartDate = request.StartDate;
        sslCertificate.ExpirationDate = request.ExpirationDate;
        sslCertificate.AutoRenew = request.AutoRenew;
        sslCertificate.DomainId = request.DomainId;
        sslCertificate.Notes = request.Notes;
        
        await _repository.UpdateAsync(sslCertificate, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}
