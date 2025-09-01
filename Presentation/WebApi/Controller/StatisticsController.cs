using Application.Features.Queries.StatisticsQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("AssetOperations")]
[Authorize(Roles = "Admin")]
public class StatisticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatisticsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("dashboard-summary")]
    public async Task<IActionResult> GetDashboardSummary()
    {
        try
        {
            var query = new GetDashboardSummaryQuery();
            var result = await _mediator.Send(query);
            
            Log.Information("Dashboard summary statistics retrieved successfully");
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error occurred while retrieving dashboard summary statistics");
            return StatusCode(500, new { message = "İstatistikler getirilirken bir hata oluştu", error = ex.Message });
        }
    }


    [HttpGet("assets/by-status")]
    public async Task<IActionResult> GetAssetsByStatus()
    {
        try
        {
            var query = new GetAssetsByStatusQuery();
            var result = await _mediator.Send(query);
            
            Log.Information("Assets by status statistics retrieved successfully");
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error occurred while retrieving assets by status statistics");
            return StatusCode(500, new { message = "Durum bazında varlık istatistikleri getirilirken bir hata oluştu", error = ex.Message });
        }
    }


    [HttpGet("assets/by-department")]
    public async Task<IActionResult> GetAssetsByDepartment()
    {
        try
        {
            var query = new GetAssetsByDepartmentQuery();
            var result = await _mediator.Send(query);
            
            Log.Information("Assets by department statistics retrieved successfully");
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error occurred while retrieving assets by department statistics");
            return StatusCode(500, new { message = "Departman bazında varlık istatistikleri getirilirken bir hata oluştu", error = ex.Message });
        }
    }


    [HttpGet("ssl-certificates/expiring")]
    public async Task<IActionResult> GetExpiringSslCertificates([FromQuery] int days = 30)
    {
        try
        {
            var query = new GetExpiringSslCertificatesQuery { Days = days };
            var result = await _mediator.Send(query);
            
            Log.Information("Expiring SSL certificates retrieved successfully for {Days} days", days);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error occurred while retrieving expiring SSL certificates for {Days} days", days);
            return StatusCode(500, new { message = "Süresi dolacak SSL sertifikaları getirilirken bir hata oluştu", error = ex.Message });
        }
    }


    [HttpGet("domains/expiring")]
    public async Task<IActionResult> GetExpiringDomains([FromQuery] int days = 30)
    {
        try
        {
            var query = new GetExpiringDomainsQuery { Days = days };
            var result = await _mediator.Send(query);
            
            Log.Information("Expiring domains retrieved successfully for {Days} days", days);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error occurred while retrieving expiring domains for {Days} days", days);
            return StatusCode(500, new { message = "Süresi dolacak domain'ler getirilirken bir hata oluştu", error = ex.Message });
        }
    }


    [HttpGet("assets/expired-warranty")]
    public async Task<IActionResult> GetAssetsWithExpiredWarranty()
    {
        try
        {
            var query = new GetAssetsWithExpiredWarrantyQuery();
            var result = await _mediator.Send(query);
            
            Log.Information("Assets with expired warranty retrieved successfully");
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error occurred while retrieving assets with expired warranty");
            return StatusCode(500, new { message = "Garanti süresi dolmuş varlıklar getirilirken bir hata oluştu", error = ex.Message });
        }
    }
}
