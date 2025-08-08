using Application.Models;
using Domain.Entites;

namespace Application.Services;

public interface ITokenHandler
{
    public Token CreateAccessToken(AppUser user, string role);
}