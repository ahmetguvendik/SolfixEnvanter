using Application.Features.Commands.MaintenanceRecordCommands;
using Application.Features.Queries.MaintenanceRecordQueries;
using Application.Features.Queries.MaintenanceTypeQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceRecordController  : ControllerBase
{
    private readonly IMediator _mediator;

    public MaintenanceRecordController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpPost("CompleteMaintenance")]
    public async Task<IActionResult> CompleteMaintenance([FromBody] CompleteMaintenanceCommand command)
    {
        await _mediator.Send(command);
        return Ok("Created");   
    }

    [HttpGet]
    public async Task<IActionResult> GetToday([FromQuery] DateTime? date)   
    {
        var values = await _mediator.Send(new GetCompleteMaintenanceRecordQuery { Date = date });
        return Ok(values);
    }
    
}