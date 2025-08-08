using Application.Features.Results;
using MediatR;

namespace Application.Features.Commands.AppUser;

public class LoginUserCommand : IRequest<LoginUserQueryResult>
{
    public string Username { get; set; }
    public string Password { get; set; }
}