namespace Application.Interfaces;

public interface ISslCertificateRepository
{
    Task<List<SslCertificate>> GetAllSslCertificatesWithDomain(); 

}