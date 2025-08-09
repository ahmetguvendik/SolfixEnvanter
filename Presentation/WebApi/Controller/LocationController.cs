using Application.Features.Commands;
using Application.Features.Queries.LocationQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class LocationController  : ControllerBase
{
    private readonly IMediator  _mediator;

    public LocationController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetLocation()
    {
        var locations = await _mediator.Send(new GetAllLocationQuery());
        return Ok(locations);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateLocation([FromBody] CreateLocationCommand command)
    {
        await _mediator.Send(command);
        return Ok("Eklendi");   
    }   
}