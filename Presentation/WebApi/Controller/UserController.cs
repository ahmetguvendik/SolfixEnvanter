using Application.Features.Commands.AppUser;
using Application.Features.Queries.AppUserQueries;
using Application.Features.Results;
using Application.Services;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using Application.Features.Commands;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is creating a new user: {NewUserName}", userId, userName, command.Username);
            
            await _mediator.Send(command);
            return Ok("Kullanici Eklendi");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Post (CreateUser)");
            return StatusCode(500, "Internal server error");
        }
    }
    
   
    [HttpPost("jwt-login")]
    [AllowAnonymous]
    public async Task<IActionResult> JwtLogin([FromBody] LoginUserCommand command)
    {
        Log.Information("Login attempt for user: {Username}", command?.Username ?? "null");
        
        if (command == null)
        {
            Log.Warning("Login attempt with null command");
            return BadRequest("Invalid request");
        }

        var result = await _mediator.Send(command);

        if (result == null || string.IsNullOrEmpty(result.Role))
        {
            Log.Warning("Login failed for user: {Username}", command.Username);
            return Unauthorized("Invalid credentials");
        }

        // Kullanıcıyı bul
        var user = await _userManager.FindByNameAsync(command.Username);
        if (user == null)
        {
            Log.Warning("User not found: {Username}", command.Username);
            return Unauthorized("User not found");
        }

        // JWT token oluştur
        var token = _tokenHandler.CreateAccessToken(user, result.Role);
        Log.Information("Login successful for user: {Username}", command.Username);

        return Ok(new
        {
            token = token.AccessToken,
            expiration = token.Expiration
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all users", userId, userName);
            
            var users = await _mediator.Send(new GetUserQuery());
            return Ok(users);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetUsers)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("GetUserById")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var users = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(users);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is updating user with ID: {UpdateUserId}", userId, userName, command.Id);
            
            await _mediator.Send(command);
            return Ok("Güncellendi");   
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Put (UpdateUser)");
            return StatusCode(500, "Internal server error");
        }
    }
    
}