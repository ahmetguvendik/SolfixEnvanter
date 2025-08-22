using Application.Features.Commands.SslCertificateCommands;
using Application.Features.Queries.SslCertificateQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is creating a new SSL certificate: {CertificateName}", userId, userName, command.CommonName);
            
            await _mediator.Send(command);
            return Ok("Ssl certificate created");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Post (CreateSslCertificate)");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetSslCertificate()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all SSL certificates", userId, userName);
            
            var values = await _mediator.Send(new GetAllSslCertificateQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllSslCertificates)");
            return StatusCode(500, "Internal server error");
        }
    }
}