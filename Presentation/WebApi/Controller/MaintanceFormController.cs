using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Application.Features.Commands.MaintanceFormCommands;
using Application.Features.Queries.MaintanceFormQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MaintanceFormController  : ControllerBase
{
    private readonly IMediator _mediator;

    public MaintanceFormController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateForm([FromForm] CreateMaintanceFormCommand command)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is creating a new maintenance form", userId, userName);
            
            await _mediator.Send(command);
            return Ok("Eklendi");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Post (CreateFaultReport)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetForm()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all maintenance forms", userId, userName);
            
            var values = await _mediator.Send(new GetMaintanceFormQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetMaintanceForm)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteForm(string id)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is deleting maintenance form with ID: {MaintenanceFormId}", userId, userName, id);
            
            await _mediator.Send(new DeleteMaintanceFormCommand { Id = id });
            return Ok("Silindi");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Delete (DeleteMaintanceForm)");
            return StatusCode(500, "Internal server error");
        }
    }
}