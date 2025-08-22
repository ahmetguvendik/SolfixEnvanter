using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Application.Features.Commands.ProcedureCommands;
using Application.Features.Queries.ProcedureQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProcedureController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProcedureController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProcedure([FromForm] CreateProcedureCommand command)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is creating a new procedure", userId, userName);
            
            await _mediator.Send(command);
            return Ok("Procedure created successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Post (CreateProcedure)");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProcedures()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all procedures", userId, userName);
            
            var values = await _mediator.Send(new GetAllProcedureQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllProcedures)");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProcedure(string id)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is deleting procedure with ID: {ProcedureId}", userId, userName, id);
            
            await _mediator.Send(new DeleteProcedureCommand { Id = id });
            return Ok("Procedure deleted successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Delete (DeleteProcedure)");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
