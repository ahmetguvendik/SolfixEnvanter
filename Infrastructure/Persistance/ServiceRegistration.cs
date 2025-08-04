using Application.Interfaces;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Contexts;
using Persistance.Repositories;

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

    }
    
}