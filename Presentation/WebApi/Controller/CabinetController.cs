using System.Security.Authentication.ExtendedProtection;
using Application.Features.Commands.CabinetCommands;
using Application.Features.Queries.CabinetQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using Application.Features.Commands;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class CabinetController : ControllerBase
{
    private readonly IMediator _mediator;

    public CabinetController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all cabinets", userId, userName);
            
            var cabinets = await _mediator.Send(new GetAllCabinetQuery());
            return Ok(cabinets);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllCabinets)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCabinetById(string id)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving cabinet with ID: {CabinetId}", userId, userName, id);
            
            var query = new GetCabinetByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetCabinetById)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCabinetCommand command)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is creating a new cabinet: {CabinetName}", userId, userName, command.Name);
            
            await _mediator.Send(command);
            return Ok("Cabinet created");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Post (CreateCabinet)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCabinetCommand command)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is updating cabinet with ID: {CabinetId}", userId, userName, command.Id);
            
            await _mediator.Send(command);
            return Ok("Güncellendi");   
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Put (UpdateCabinet)");
            return StatusCode(500, "Internal server error");
        }
    }
}