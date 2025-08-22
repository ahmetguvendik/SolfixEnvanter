using Application.Features.Commands.InternetLinesCommands;
using Application.Features.Queries.InternetLinesQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
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
}