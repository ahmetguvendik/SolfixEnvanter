using Application.Features.Commands.MaintenanceTypeCommands;
using Application.Features.Queries.MaintanceTypeQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class MaintanceTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public MaintanceTypeController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMaintenanceTypeCommand command)
    {
        await _mediator.Send(command);
        return Ok("Successfully created maintenance type");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var values = await _mediator.Send(new GetAllMaintanceTypeQuery());
        return Ok(values);
    }
}