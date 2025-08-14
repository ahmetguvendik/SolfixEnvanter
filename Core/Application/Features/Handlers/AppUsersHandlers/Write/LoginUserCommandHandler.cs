using Application.Features.Commands.AppUser;
using Application.Features.Results;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Handlers.AppUsers.Write;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserQueryResult>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;
    public LoginUserCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<LoginUserQueryResult?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var appUser = await _userManager.FindByNameAsync(request.Username);
        if (appUser == null)
            return null;

        var response = await _signInManager.PasswordSignInAsync(appUser, request.Password, false, false);
        if (!response.Succeeded)
            return null;

        var role = (await _userManager.GetRolesAsync(appUser)).FirstOrDefault();
        if (role == null)
            return null; 

        return new LoginUserQueryResult
        {
            Id = appUser.Id,
            Username = appUser.UserName,
            DepartmanId = appUser.DepartmentId,
            Role = role
        };
    }

}