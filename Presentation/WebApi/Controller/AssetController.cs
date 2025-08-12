using Application.Features.Commands;
using Application.Features.Queries.AssetQueries;
using Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
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
         await _mediator.Send(asset);
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
    
}