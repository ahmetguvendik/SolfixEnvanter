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
}