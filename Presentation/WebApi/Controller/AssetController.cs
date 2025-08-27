using Application.Features.Commands;
using Application.Features.Queries.AssetQueries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("AssetOperations")]
public class AssetController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsset([FromBody] CreateAssetCommand asset)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is creating a new asset: {AssetName}", userId, userName, asset.Name);
            
            await _mediator.Send(asset);
            return Ok("Eklendi");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Post (CreateAsset)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving asset by id", userId, userName);
            
            var values = await _mediator.Send(new GetAssetByIdQuery(id));
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllDesktop)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("GetAllDesktop")]
    public async Task<IActionResult> GetAllDesktop()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all desktop assets", userId, userName);
            
            var values = await _mediator.Send(new GetAllDesktopQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllDesktop)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("GetAllLaptop")]
    public async Task<IActionResult> GetAllLaptop()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all laptop assets", userId, userName);
            
            var values = await _mediator.Send(new GetAllLaptopQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllLaptop)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("GetAllPrinter")]
    public async Task<IActionResult> GetAllPrinter()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all printer assets", userId, userName);
            
            var values = await _mediator.Send(new GetAllPrinterQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllPrinter)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("GetAllMouse")]
    public async Task<IActionResult> GetAllMouse()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all mouse assets", userId, userName);
            
            var values = await _mediator.Send(new GetAllMouseQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllMouse)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("GetAllKeyboard")]
    public async Task<IActionResult> GetAllKeyboard()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all keyboard assets", userId, userName);
            
            var values = await _mediator.Send(new GetAllKeyboardQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllKeyboard)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("GetAllKSwitch")]
    public async Task<IActionResult> GetAllKSwitch()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all switch assets", userId, userName);
            
            var values = await _mediator.Send(new GetAllSwitchQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllKSwitch)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("GetAllRouter")]
    public async Task<IActionResult> GetAllRouter()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all router assets", userId, userName);
            
            var values = await _mediator.Send(new GetAllRouterQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllRouter)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("GetAllAp")]
    public async Task<IActionResult> GetAllAp()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all access point assets", userId, userName);
            
            var values = await _mediator.Send(new GetAllApQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllAp)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("GetAllFirewall")]
    public async Task<IActionResult> GetAllFirewall()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all firewall assets", userId, userName);
            
            var values = await _mediator.Send(new GetAllFirewallQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllFirewall)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("GetAllModem")]
    public async Task<IActionResult> GetAllModem()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all modem assets", userId, userName);
            
            var values = await _mediator.Send(new GetAllModemQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllModem)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("GetAllWindowsKey")]
    public async Task<IActionResult> GetAllWindowsKey()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all Windows key assets", userId, userName);
            
            var values = await _mediator.Send(new GetAllWindowsKeyQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllWindowsKey)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("GetAllSystemSoftware")]
    public async Task<IActionResult> GetAllSystemSoftware()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all system software assets", userId, userName);
            
            var values = await _mediator.Send(new GetAllSystemSoftwareQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllSystemSoftware)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("GetAllTV")]
    public async Task<IActionResult> GetAllTV()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all TV assets", userId, userName);
            
            var values = await _mediator.Send(new GetAllTVQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllTV)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("GetAllAssignedAssetUser")]
    public async Task<IActionResult> GetAllAssignedAssetUser()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all assigned asset users", userId, userName);
            
            var values = await _mediator.Send(new GetAllAssignedAssetUserQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllAssignedAssetUser)");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("GetAllAssets")]
    public async Task<IActionResult> GetAllAssets()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all assets", userId, userName);
            
            var values = await _mediator.Send(new GetAllAssetsQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllAssets)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsset([FromBody] UpdateAssetCommand command)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is updating asset with ID: {AssetId}, Status: {Status}", userId, userName, command.Id, command.Status);
            Console.WriteLine($"UpdateAsset - ID: {command.Id}, Status: {command.Status}, HasValue: {command.Status.HasValue}");
            
            await _mediator.Send(command);
            return Ok("Güncellendi");   
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Put (UpdateAsset)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPatch("assign")]
    public async Task<IActionResult> AssignAssetToUser([FromBody] AssignAssetToUserCommand command)
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is assigning asset {AssetId} to user {TargetUserId}", userId, userName, command.AssetId, command.UserId ?? "null");
            
            await _mediator.Send(command);
            return Ok("Asset zimmetlendi");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Patch (AssignAssetToUser)");
            return StatusCode(500, "Internal server error");
        }
    }
    
}