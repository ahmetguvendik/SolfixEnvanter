using Domain.Entites;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task<AppUser> GetUserById(string id); 
}