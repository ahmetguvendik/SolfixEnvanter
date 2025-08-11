using Application.Features.Commands.SslCertificateCommands;
using Application.Features.Queries.SslCertificateQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class SslCertificateController : ControllerBase
{
    private readonly IMediator _mediator;

    public SslCertificateController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateSslCertificateCommand command)
    {
        await _mediator.Send(command);
        return Ok("Ssl certificate created");
    }

    [HttpGet]
    public async Task<IActionResult> GetSslCertificate()
    {
        var values = await _mediator.Send(new GetAllSslCertificateQuery());
        return Ok(values);
    }
}