using Application.Features.Commands.MaintenanceTypeCommands;
using Application.Features.Queries.MaintenanceTypeQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public MaintenanceTypeController(IMediator mediator)
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
        var values = await _mediator.Send(new GetAllMaintenanceTypeQuery());
        return Ok(values);
    }
}