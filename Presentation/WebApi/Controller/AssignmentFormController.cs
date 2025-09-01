using Application.Features.Commands.AssignmentFormCommands;
using Application.Features.Queries.AssignmentFormQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AssignmentFormController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssignmentFormController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAssignmentForm([FromForm] CreateAssignmentFormCommand command)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is creating a new assignment form", userId, userName);
            
            await _mediator.Send(command);
            return Ok("Assignment form created successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Post (CreateAssignmentForm)");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAssignmentForms()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all assignment forms", userId, userName);
            
            var values = await _mediator.Send(new GetAllAssignmentFormQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllAssignmentForms)");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssignmentForm(string id)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is deleting assignment form with ID: {AssignmentFormId}", userId, userName, id);
            
            await _mediator.Send(new DeleteAssignmentFormCommand { Id = id });
            return Ok("Assignment form deleted successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Delete (DeleteAssignmentForm)");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
