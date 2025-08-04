using Application.Features.Commands;
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
    
    [HttpPost]
    public async Task<IActionResult> CreateLocation([FromBody] CreateLocationCommand command)
    {
        await _mediator.Send(command);
        return Ok("Eklendi");   
    }
}