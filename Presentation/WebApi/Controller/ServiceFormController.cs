using Application.Features.Commands.ServiceFormCommands;
using Application.Features.Queries.ServiceFormQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ServiceFormController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiceFormController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateServiceForm([FromForm] CreateServiceFormCommand command)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is creating a new service form", userId, userName);
            
            await _mediator.Send(command);
            return Ok("Service form created successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Post (CreateServiceForm)");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllServiceForms()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all service forms", userId, userName);
            
            var values = await _mediator.Send(new GetAllServiceFormQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllServiceForms)");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServiceForm(string id)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is deleting service form with ID: {ServiceFormId}", userId, userName, id);
            
            await _mediator.Send(new DeleteServiceFormCommand { Id = id });
            return Ok("Service form deleted successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Delete (DeleteServiceForm)");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
