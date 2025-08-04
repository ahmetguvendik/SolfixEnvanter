using Application.Features.Commands;
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
}