using Application.Features.Queries.DepartmentQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DepartmentController  : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentController(IMediator mediator)
    {
         _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var userName = User?.Identity?.Name ?? "Anonymous";
            
            Log.Information("User {UserId} ({UserName}) is retrieving all departments", userId, userName);
            
            var values = await _mediator.Send(new GetDepartmentQuery());
            return Ok(values);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error in Get (GetAllDepartments)");
            return StatusCode(500, "Internal server error");
        }
    }
}