using Application.Features.Commands.MaintenanceTypeCommands;
using Application.Features.Queries.MaintenanceTypeQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MaintenanceTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public MaintenanceTypeController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMaintenanceTypeCommand command)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is creating a new maintenance type: {MaintenanceTypeName}", userId, userName, command.Name);
            
            await _mediator.Send(command);
            return Ok("Successfully created maintenance type");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Post (CreateMaintenanceType)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all maintenance types", userId, userName);
            
            var values = await _mediator.Send(new GetAllMaintenanceTypeQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllMaintenanceTypes)");
            return StatusCode(500, "Internal server error");
        }
    }
}