using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance.Contexts;

namespace Persistance.Repositories;

public class GenericRepository<T> : IGenericRepository<T>  where T : class
{
    private readonly SolfixEnvanterDbContext  _context;

    public GenericRepository(SolfixEnvanterDbContext context)
    {
         _context = context;
    }
    
    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync(); 
    }

    public async Task<T> GetByIdAsync(string id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        return Task.CompletedTask;
    }
    

    public Task RemoveAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }
    

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}