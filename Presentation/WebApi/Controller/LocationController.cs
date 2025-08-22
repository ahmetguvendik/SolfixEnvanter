using Application.Features.Commands;
using Application.Features.Queries.LocationQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LocationController  : ControllerBase
{
    private readonly IMediator  _mediator;

    public LocationController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetLocation()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all locations", userId, userName);
            
            var locations = await _mediator.Send(new GetAllLocationQuery());
            return Ok(locations);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllLocations)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateLocation([FromBody] CreateLocationCommand command)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is creating a new location: {LocationName}", userId, userName, command.Name);
            
            await _mediator.Send(command);
            return Ok("Eklendi");   
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Post (CreateLocation)");
            return StatusCode(500, "Internal server error");
        }
    }   
}