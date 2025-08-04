using Domain.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Contexts;

public class SolfixEnvanterDbContext: IdentityDbContext<AppUser, AppRole, string>
{
    public SolfixEnvanterDbContext(DbContextOptions<SolfixEnvanterDbContext> options) : base(options) { }

    
}