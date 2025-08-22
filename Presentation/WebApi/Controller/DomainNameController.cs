using Application.Features.Commands.DomainNameCommands;
using Application.Features.Queries.DomainNameQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is creating a new domain name: {DomainName}", userId, userName, domainName.Name);
            
            await _mediator.Send(domainName);
            return Ok("Domain name created");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Post (CreateDomainName)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetDomainName()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all domain names", userId, userName);
            
            var values = await _mediator.Send(new GetAllDomainNameQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllDomainNames)");
            return StatusCode(500, "Internal server error");
        }
    }
}