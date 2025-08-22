using Application.Features.Commands.MaintanceFormCommands;
using Application.Features.Queries.MaintanceFormQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class MaintanceFormController  : ControllerBase
{
    private readonly IMediator _mediator;

    public MaintanceFormController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateForm([FromForm] CreateMaintanceFormCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok("Eklendi");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Post (CreateFaultReport)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetForm()
    {
        var values = await _mediator.Send(new GetMaintanceFormQuery());
        return Ok(values);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteForm(string id)
    {
        try
        {
            await _mediator.Send(new DeleteMaintanceFormCommand { Id = id });
            return Ok("Silindi");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Delete (DeleteMaintanceForm)");
            return StatusCode(500, "Internal server error");
        }
    }
}