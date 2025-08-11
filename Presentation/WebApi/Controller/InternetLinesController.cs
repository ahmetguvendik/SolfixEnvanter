using Application.Features.Commands.InternetLinesCommands;
using Application.Features.Queries.InternetLinesQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class InternetLinesController : ControllerBase
{
    private readonly IMediator _mediator;

    public InternetLinesController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateInternetLines(CreateInternetLinesCommad command)
    {
        await _mediator.Send(command);
        return Ok("Successfully created new internet line");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInternetLines()
    {
        var values = await _mediator.Send(new GetAllInternetLinesQuery());
        return Ok(values);
    }
}