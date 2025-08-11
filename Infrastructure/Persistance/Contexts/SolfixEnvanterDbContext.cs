using Domain.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Contexts;

public class SolfixEnvanterDbContext: IdentityDbContext<AppUser, AppRole, string>
{
    public SolfixEnvanterDbContext(DbContextOptions<SolfixEnvanterDbContext> options) : base(options) { }

    public DbSet<Asset> Assets { get; set; }
    public DbSet<AssetType> AssetTypes { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<InternetLine> InternetLines { get; set; }
}