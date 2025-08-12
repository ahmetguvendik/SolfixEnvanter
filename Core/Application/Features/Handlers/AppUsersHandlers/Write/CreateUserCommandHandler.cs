using Application.Features.Commands.AppUser;
using Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Handlers.AppUsers.Write;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public CreateUserCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
         _userManager = userManager;
         _roleManager = roleManager;
    }
    
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var appUser = new AppUser();
        appUser.Id = Guid.NewGuid().ToString();
        appUser.UserName = request.Username;    
        appUser.FullName = request.NameSurname;
        appUser.DepartmentId = request.DepartmanId;
        appUser.Email = request.Email;
        var response = await _userManager.CreateAsync(appUser, request.Password);
        if (response.Succeeded)
        {
            var role = await _roleManager.FindByNameAsync(request.Role);
            if (role == null)
            {
                var appRole = new AppRole()
                {
                    Name = "Staff",
                };
                await _roleManager.CreateAsync(appRole);
                await _userManager.AddToRoleAsync(appUser, "Staff");    
            }

            await _userManager.AddToRoleAsync(appUser, request.Role);    
                
        }
        else
        {
            throw new Exception("Kullanıcı oluşturulamadı. Lütfen girdiğiniz bilgileri kontrol edin.");
        }
    }
}