using Application.Interfaces;
using Application.Models;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Repositories;
using Persistence.Contexts;
using Persistence.Repositories;
using Persistence.Services;


namespace Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceService(this IServiceCollection collection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        collection.AddDbContext<SolfixEnvanterDbContext>(opt =>
            opt.UseSqlServer(connectionString));

        collection.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<SolfixEnvanterDbContext>() 
            .AddDefaultTokenProviders();
    
        //Interfaces
        collection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));    
        collection.AddScoped(typeof(IUserRepository), typeof(UserRepository));    
        collection.AddScoped(typeof(IAssetRepository), typeof(AssetRepository));    
        collection.AddScoped(typeof(IInternetLinesRepository), typeof(InternetLinesRepository));    
        collection.AddScoped(typeof(ISslCertificateRepository), typeof(SslCertificateRepository));    
        collection.AddScoped(typeof(ICabinetRepository), typeof(CabinetRepository));    
        collection.AddScoped(typeof(IAssetNetworkInfoRepository), typeof(AssetNetworkInfoRepository));    
        collection.AddScoped(typeof(IMaintenanceRecordRepository), typeof(MaintenanceRecordRepository));    

        
        //Services
        collection.AddScoped(typeof(IMaintenanceService), typeof(MaintenanceService));    
        collection.AddScoped<ITokenHandler, TokenHandler>();

        var jwtSection = configuration.GetSection("Jwt");
        var jwtSettings = jwtSection.Get<JwtSettings>();
        collection.Configure<JwtSettings>(options =>
        {
            options.Key = jwtSettings.Key;
            options.Issuer = jwtSettings.Issuer;
            options.Audience = jwtSettings.Audience;
            options.ExpireMinutes = jwtSettings.ExpireMinutes;
        });
    }

    
}