using Application.Features.Commands.MaintenanceRecordCommands;
using Application.Features.Queries.MaintenanceRecordQueries;
using Application.Features.Queries.MaintenanceTypeQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class MaintenanceRecordController  : ControllerBase
{
    private readonly IMediator _mediator;

    public MaintenanceRecordController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpPost("CompleteMaintenance")]
    public async Task<IActionResult> CompleteMaintenance([FromBody] CompleteMaintenanceCommand command)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is completing maintenance for asset: {AssetId}", userId, userName, command.maintenanceTypeId);
            
            await _mediator.Send(command);
            return Ok("Created");   
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Post (CompleteMaintenance)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetToday([FromQuery] DateTime? date)   
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving maintenance records for date: {Date}", userId, userName, date?.ToString("yyyy-MM-dd") ?? "today");
            
            var values = await _mediator.Send(new GetCompleteMaintenanceRecordQuery { Date = date });
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetToday)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("GetAllMaintenanceRecords")]
    public async Task<IActionResult> GetAll()   
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all maintenance records", userId, userName);
            
            var values = await _mediator.Send(new GetAllCompleteMaintenanceRecordQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllMaintenanceRecords)");
            return StatusCode(500, "Internal server error");
        }
    }
    
}