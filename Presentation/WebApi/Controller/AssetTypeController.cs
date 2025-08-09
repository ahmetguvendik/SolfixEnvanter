using Application.Features.Queries.AssetTypeQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
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
        var results = await _mediator.Send(new GetAssetTypeQuery());
        return Ok(results);
    }
}