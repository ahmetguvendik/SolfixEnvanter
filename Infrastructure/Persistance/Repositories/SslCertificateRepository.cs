using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;
 
public class SslCertificateRepository  : ISslCertificateRepository
{
    private readonly SolfixEnvanterDbContext  _dbContext;

    public SslCertificateRepository(SolfixEnvanterDbContext dbContext)
    {
         _dbContext = dbContext;
    }
    
    public async Task<List<SslCertificate>>GetAllSslCertificatesWithDomain()
    {
       var values = await _dbContext.SslCertificates.Include(x=>x.Domain).ToListAsync();
       return values;
    }
}