using Application.Features.Commands.AssetNetworkInfoCommands;
using Application.Features.Queries.AssetNetworkInfoQueries;
using Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
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
        await _mediator.Send(command);
        return Ok("Success");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAssetNetworkInfos()
    {
        var values = await _mediator.Send(new GetAllAssetNetworkInfoQuery());
        return Ok(values);
    }
}