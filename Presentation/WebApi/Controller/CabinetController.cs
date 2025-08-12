using System.Security.Authentication.ExtendedProtection;
using Application.Features.Commands.CabinetCommands;
using Application.Features.Queries.CabinetQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class CabinetController : ControllerBase
{
    private readonly IMediator _mediator;

    public CabinetController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var cabinets = await _mediator.Send(new GetAllCabinetQuery());
        return Ok(cabinets);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCabinetCommand command)
    {
        await _mediator.Send(command);
        return Ok("Cabinet created");
    }
}