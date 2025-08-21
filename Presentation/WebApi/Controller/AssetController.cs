using Application.Features.Commands;
using Application.Features.Queries.AssetQueries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class AssetController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AssetController> _logger;

    public AssetController(IMediator mediator, ILogger<AssetController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsset([FromBody] CreateAssetCommand asset)
    {
        _logger.LogInformation("Creating asset: {AssetName}", asset.Name);
        await _mediator.Send(asset);
        _logger.LogInformation("Asset created successfully: {AssetName}", asset.Name);
        return Ok("Eklendi");
    }

    [HttpGet("GetAllDesktop")]
    public async Task<IActionResult> GetAllDesktop()
    {
        var values = await _mediator.Send(new GetAllDesktopQuery());
        return Ok(values);
    }
    
    [HttpGet("GetAllLaptop")]
    public async Task<IActionResult> GetAllLaptop()
    {
        var values = await _mediator.Send(new GetAllLaptopQuery());
        return Ok(values);
    }
    
    [HttpGet("GetAllPrinter")]
    public async Task<IActionResult> GetAllPrinter()
    {
        var values = await _mediator.Send(new GetAllPrinterQuery());
        return Ok(values);
    }
    
    [HttpGet("GetAllMouse")]
    public async Task<IActionResult> GetAllMouse()
    {
        var values = await _mediator.Send(new GetAllMouseQuery());
        return Ok(values);
    }
    
    [HttpGet("GetAllKeyboard")]
    public async Task<IActionResult> GetAllKeyboard()
    {
        var values = await _mediator.Send(new GetAllKeyboardQuery());
        return Ok(values);
    }
    
    [HttpGet("GetAllKSwitch")]
    public async Task<IActionResult> GetAllKSwitch()
    {
        var values = await _mediator.Send(new GetAllSwitchQuery());
        return Ok(values);
    }
    
    [HttpGet("GetAllRouter")]
    public async Task<IActionResult> GetAllRouter()
    {
        var values = await _mediator.Send(new GetAllRouterQuery());
        return Ok(values);
    }
    
    [HttpGet("GetAllAp")]
    public async Task<IActionResult> GetAllAp()
    {
        var values = await _mediator.Send(new GetAllApQuery());
        return Ok(values);
    }
    
    [HttpGet("GetAllFirewall")]
    public async Task<IActionResult> GetAllFirewall()
    {
        var values = await _mediator.Send(new GetAllFirewallQuery());
        return Ok(values);
    }
    
    [HttpGet("GetAllModem")]
    public async Task<IActionResult> GetAllModem()
    {
        var values = await _mediator.Send(new GetAllModemQuery());
        return Ok(values);
    }
    
    [HttpGet("GetAllWindowsKey")]
    public async Task<IActionResult> GetAllWindowsKey()
    {
        var values = await _mediator.Send(new GetAllWindowsKeyQuery());
        return Ok(values);
    }
    
    [HttpGet("GetAllSystemSoftware")]
    public async Task<IActionResult> GetAllSystemSoftware()
    {
        var values = await _mediator.Send(new GetAllSystemSoftwareQuery());
        return Ok(values);
    }
    
    [HttpGet("GetAllTV")]
    public async Task<IActionResult> GetAllTV()
    {
        var values = await _mediator.Send(new GetAllTVQuery());
        return Ok(values);
    }
    
    [HttpGet("GetAllAssignedAssetUser")]
    public async Task<IActionResult> GetAllAssignedAssetUser()
    {
        var values = await _mediator.Send(new GetAllAssignedAssetUserQuery());
        return Ok(values);
    }
    
    [HttpGet("GetAllAssets")]
    public async Task<IActionResult> GetAllAssets()
    {
        _logger.LogInformation("Getting all assets");
        var values = await _mediator.Send(new GetAllAssetsQuery());
        _logger.LogInformation("Retrieved {Count} assets", values?.Count ?? 0);
        return Ok(values);
    }
    
    [HttpGet("test-logging")]
    public IActionResult TestLogging()
    {
        _logger.LogTrace("This is a trace message");
        _logger.LogDebug("This is a debug message");
        _logger.LogInformation("This is an information message");
        _logger.LogWarning("This is a warning message");
        _logger.LogError("This is an error message");
        _logger.LogCritical("This is a critical message");
        
        return Ok("Logging test completed. Check console and database for logs.");
    }
    
}