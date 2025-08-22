using Application.Features.Commands.AssetNetworkInfoCommands;
using Application.Features.Queries.AssetNetworkInfoQueries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AssetNetworkInfoController : ControllerBase
{
    private readonly IMediator  _mediator;

    public AssetNetworkInfoController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAssetNetworkInfo([FromBody] CreateAssetNetworkInfoCommand command)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is creating a new asset network info for asset: {AssetId}", userId, userName, command.AssetId);
            
            await _mediator.Send(command);
            return Ok("Success");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Post (CreateAssetNetworkInfo)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAssetNetworkInfos()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all asset network infos", userId, userName);
            
            var values = await _mediator.Send(new GetAllAssetNetworkInfoQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllAssetNetworkInfos)");
            return StatusCode(500, "Internal server error");
        }
    }
}