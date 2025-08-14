using Domain.Entities;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task<AppUser> GetUserById(string id); 
}