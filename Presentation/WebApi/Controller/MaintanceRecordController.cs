using Application.Features.Commands.MaintenanceRecordCommands;
using Application.Features.Queries.MaintanceRecordQueries;
using Application.Features.Queries.MaintanceTypeQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class MaintanceRecordController  : ControllerBase
{
    private readonly IMediator _mediator;

    public MaintanceRecordController(IMediator mediator)
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
    public async Task<IActionResult>GetAllRecordsWithUserAndType()   
    {
        var values = await _mediator.Send(new GetCompleteMaintanceRecordQuery());
        return Ok(values);
    }
    
}