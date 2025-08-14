using Application.Models;
using Domain.Entities;

namespace Application.Services;

public interface ITokenHandler
{
    public Token CreateAccessToken(AppUser user, string role);
}