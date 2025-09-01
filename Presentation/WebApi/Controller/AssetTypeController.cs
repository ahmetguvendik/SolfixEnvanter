using Application.Features.Queries.AssetTypeQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AssetTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetTypeController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAssetType()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all asset types", userId, userName);
            
            var results = await _mediator.Send(new GetAssetTypeQuery());
            return Ok(results);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllAssetTypes)");
            return StatusCode(500, "Internal server error");
        }
    }
}