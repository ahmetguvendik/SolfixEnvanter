using Application.Features.Commands.AppUser;
using Application.Features.Queries.AppUserQueries;
using Application.Features.Results;
using Application.Services;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITokenHandler _tokenHandler;
    private readonly UserManager<AppUser> _userManager;


    public UserController(IMediator mediator, ITokenHandler tokenHandler, UserManager<AppUser> userManager)
    {
          _mediator = mediator;
          _tokenHandler = tokenHandler;
          _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        await _mediator.Send(command);
        return Ok("Kullanici Eklendi");
    }
    
   
    [HttpPost("jwt-login")]
    public async Task<IActionResult> JwtLogin([FromBody] LoginUserCommand command)
    {
        if (command == null)
            return BadRequest("Invalid request");

        var result = await _mediator.Send(command);

        if (result == null || string.IsNullOrEmpty(result.Role))
            return Unauthorized("Invalid credentials");

        // Kullanıcıyı bul
        var user = await _userManager.FindByNameAsync(command.Username);
        if (user == null)
            return Unauthorized("User not found");

        // JWT token oluştur
        var token = _tokenHandler.CreateAccessToken(user, result.Role);

        return Ok(new
        {
            token = token.AccessToken,
            expiration = token.Expiration
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _mediator.Send(new GetUserQuery());
        return Ok(users);
    }
    
    [HttpGet("GetUserById")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var users = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(users);
    }
}