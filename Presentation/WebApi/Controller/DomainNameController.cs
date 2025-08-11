using Application.Features.Commands.DomainNameCommands;
using Application.Features.Queries.DomainNameQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class DomainNameController  : ControllerBase
{
    private readonly IMediator _mediator;

    public DomainNameController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDomainNameCommand domainName)
    {
        await _mediator.Send(domainName);
        return Ok("Domain name created");
    }

    [HttpGet]
    public async Task<IActionResult> GetDomainName()
    {
        var values = await _mediator.Send(new GetAllDomainNameQuery());
        return Ok(values);
    }
}