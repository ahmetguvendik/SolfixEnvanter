using Application.Interfaces;
using Application.Models;
using Application.Services;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Contexts;
using Persistance.Repositories;
using Persistance.Services;


namespace Persistance;

public static class ServiceRegistration
{
    public static void AddPersistanceService(this IServiceCollection collection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        collection.AddDbContext<SolfixEnvanterDbContext>(opt =>
            opt.UseSqlServer(connectionString));

        collection.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<SolfixEnvanterDbContext>() 
            .AddDefaultTokenProviders();
    
        collection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));    
        collection.AddScoped(typeof(IUserRepository), typeof(UserRepository));    
        collection.AddScoped(typeof(IAssetRepository), typeof(AssetRepository));    
        collection.AddScoped(typeof(IInternetLinesRepository), typeof(InternetLinesRepository));    
        collection.AddScoped(typeof(ISslCertificateRepository), typeof(SslCertificateRepository));    
        collection.AddScoped(typeof(ICabinetRepository), typeof(CabinetRepository));    
        collection.AddScoped(typeof(IAssetNetworkInfoRepository), typeof(AssetNetworkInfoRepository));    


        
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