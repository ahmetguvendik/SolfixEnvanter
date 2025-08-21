using Application.Features.Commands.AppUser;
using Application.Features.Queries.AppUserQueries;
using Application.Features.Results;
using Application.Services;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITokenHandler _tokenHandler;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<UserController> _logger;


    public UserController(IMediator mediator, ITokenHandler tokenHandler, UserManager<AppUser> userManager, ILogger<UserController> logger)
    {
          _mediator = mediator;
          _tokenHandler = tokenHandler;
          _userManager = userManager;
          _logger = logger;
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
        _logger.LogInformation("Login attempt for user: {Username}", command?.Username ?? "null");
        
        if (command == null)
        {
            _logger.LogWarning("Login attempt with null command");
            return BadRequest("Invalid request");
        }

        var result = await _mediator.Send(command);

        if (result == null || string.IsNullOrEmpty(result.Role))
        {
            _logger.LogWarning("Login failed for user: {Username}", command.Username);
            return Unauthorized("Invalid credentials");
        }

        // Kullanıcıyı bul
        var user = await _userManager.FindByNameAsync(command.Username);
        if (user == null)
        {
            _logger.LogWarning("User not found: {Username}", command.Username);
            return Unauthorized("User not found");
        }

        // JWT token oluştur
        var token = _tokenHandler.CreateAccessToken(user, result.Role);
        _logger.LogInformation("Login successful for user: {Username}", command.Username);

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
    
    [HttpGet("test-db-logging")]
    public IActionResult TestDbLogging()
    {
        _logger.LogTrace("DB Test - Trace message");
        _logger.LogDebug("DB Test - Debug message");
        _logger.LogInformation("DB Test - Information message");
        _logger.LogWarning("DB Test - Warning message");
        _logger.LogError("DB Test - Error message");
        _logger.LogCritical("DB Test - Critical message");
        
        return Ok("Database logging test completed. Check the Logs table in your database.");
    }
}