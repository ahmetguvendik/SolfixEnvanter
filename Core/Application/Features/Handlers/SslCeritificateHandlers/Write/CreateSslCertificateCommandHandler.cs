using Application.Features.Commands.SslCertificateCommands;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.SslCeritificateHandlers.Write;

public class CreateSslCertificateCommandHandler : IRequestHandler<CreateSslCertificateCommand>
{
    private readonly IGenericRepository<SslCertificate> _repository;

    public CreateSslCertificateCommandHandler(IGenericRepository<SslCertificate> repository)
    {
         _repository = repository;
    }
    
    public async Task Handle(CreateSslCertificateCommand request, CancellationToken cancellationToken)
    {
        var ssl = new SslCertificate();
        ssl.Id = Guid.NewGuid().ToString();
        ssl.AutoRenew = request.AutoRenew;
        ssl.ExpirationDate = request.ExpirationDate;
        ssl.Notes = request.Notes;
        ssl.Provider = request.Provider;
        ssl.CommonName = request.CommonName;
        ssl.DomainId =  request.DomainId;
        ssl.StartDate = request.StartDate;
        await _repository.CreateAsync(ssl);
        await _repository.SaveChangesAsync();
    }
}