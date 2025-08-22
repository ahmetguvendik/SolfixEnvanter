using Application.Features.Commands.AssignmentFormCommands;
using Application.Features.Queries.AssignmentFormQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class AssignmentFormController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssignmentFormController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAssignmentForm([FromForm] CreateAssignmentFormCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok("Assignment form created successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Post (CreateAssignmentForm)");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAssignmentForms()
    {
        try
        {
            var values = await _mediator.Send(new GetAllAssignmentFormQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllAssignmentForms)");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssignmentForm(string id)
    {
        try
        {
            await _mediator.Send(new DeleteAssignmentFormCommand { Id = id });
            return Ok("Assignment form deleted successfully");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Delete (DeleteAssignmentForm)");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
