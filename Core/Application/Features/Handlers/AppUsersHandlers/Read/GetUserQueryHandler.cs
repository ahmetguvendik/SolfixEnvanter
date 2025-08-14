using Application.Features.Queries.AppUserQueries;
using Application.Features.Results;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Application.Features.Handlers.AppUsers.Read;

public class GetUserQueryHandler  : IRequestHandler<GetUserQuery, List<GetUserQueryResult>>
{
    private readonly UserManager<AppUser> _userManager;

    public GetUserQueryHandler(UserManager<AppUser> userManager)
    {
         _userManager = userManager;
    }
    
    
    public  async Task<List<GetUserQueryResult>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users
            .Select(u => new GetUserQueryResult
            {
                Id = u.Id,
                FullName = u.FullName
            })
            .ToListAsync(cancellationToken);


        return users;
    }
}