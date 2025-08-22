using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

public class SolfixEnvanterDbContext: IdentityDbContext<AppUser, AppRole, string>
{
    public SolfixEnvanterDbContext(DbContextOptions<SolfixEnvanterDbContext> options) : base(options) { }

    public DbSet<Asset> Assets { get; set; }
    public DbSet<AssetType> AssetTypes { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<InternetLine> InternetLines { get; set; }
    public DbSet<SslCertificate> SslCertificates { get; set; }
    public DbSet<DomainName> DomainNames { get; set; }
    public DbSet<Cabinet> Cabinets { get; set; }
    public DbSet<AssetNetworkInfo> AssetNetworkInfos { get; set; }
    public DbSet<MaintenanceType> MaintenanceTypes { get; set; }    
    public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
    public DbSet<MaintanceForm> MaintanceForms { get; set; }    
    public DbSet<Procedure> Procedures { get; set; }    
    public DbSet<AssignmentForm> AssignmentForms { get; set; }    
    public DbSet<ServiceForm> ServiceForms { get; set; }    
}