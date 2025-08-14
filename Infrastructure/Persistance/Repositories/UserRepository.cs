using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistance.Repositories;

public class UserRepository  : IUserRepository
{
    private readonly SolfixEnvanterDbContext  _dbContext;

    public UserRepository(SolfixEnvanterDbContext dbContext)
    {
         _dbContext = dbContext;
    }
    
    public async Task<AppUser?> GetUserById(string id)
    {
        var user = await _dbContext.Users.Include(x=>x.Department).FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }
}