using Application.Features.Commands.InternetLinesCommands;
using Application.Features.Queries.InternetLinesQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using Application.Features.Commands;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InternetLinesController : ControllerBase
{
    private readonly IMediator _mediator;

    public InternetLinesController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateInternetLines(CreateInternetLinesCommad command)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is creating a new internet line: {InternetLineName}", userId, userName, command.LineNumber);
            
            await _mediator.Send(command);
            return Ok("Successfully created new internet line");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Post (CreateInternetLines)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInternetLines()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all internet lines", userId, userName);
            
            var values = await _mediator.Send(new GetAllInternetLinesQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllInternetLines)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInternetLineById(string id)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving internet line with ID: {InternetLineId}", userId, userName, id);
            
            var query = new GetInternetLineByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetInternetLineById)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateInternetLinesCommand command)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is updating internet line with ID: {InternetLineId}", userId, userName, command.Id);
            
            await _mediator.Send(command);
            return Ok("Güncellendi");   
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Put (UpdateInternetLines)");
            return StatusCode(500, "Internal server error");
        }
    }
}