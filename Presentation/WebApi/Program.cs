using System.Security.Claims;
using System.Text;
using Persistence; 
using Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Sinks.MSSqlServer;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Application.Models;

var builder = WebApplication.CreateBuilder(args);

// Serilog bootstrap
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Filter.ByExcluding(e => 
        e.Properties.ContainsKey("SourceContext") && 
        (e.Properties["SourceContext"].ToString().Contains("MediatR") ||
         e.Properties["SourceContext"].ToString().Contains("MediatR.Mediator") ||
         e.Properties["SourceContext"].ToString().Contains("MediatR.Pipeline")))
    .Filter.ByExcluding(e => e.MessageTemplate.Text.Contains("Handling") || e.MessageTemplate.Text.Contains("Handled"))
    .CreateLogger();

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .Filter.ByExcluding(e => 
        e.Properties.ContainsKey("SourceContext") && 
        (e.Properties["SourceContext"].ToString().Contains("MediatR") ||
         e.Properties["SourceContext"].ToString().Contains("MediatR.Mediator") ||
         e.Properties["SourceContext"].ToString().Contains("MediatR.Pipeline")))
    .Filter.ByExcluding(e => e.MessageTemplate.Text.Contains("Handling") || e.MessageTemplate.Text.Contains("Handled")));


builder.Services.AddControllers();
builder.Services.AddHttpClient(); 
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor(); 

builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddApplicationService(builder.Configuration);

// Rate Limiting Configuration
var rateLimitingSettings = builder.Configuration.GetSection("RateLimiting").Get<RateLimitingSettings>() ?? new RateLimitingSettings();

builder.Services.AddRateLimiter(options =>
{
    // IP bazlı rate limiting
    options.AddFixedWindowLimiter("General", limiterOptions =>
    {
        limiterOptions.PermitLimit = rateLimitingSettings.General.PermitLimit;
        limiterOptions.Window = rateLimitingSettings.General.Window;
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = rateLimitingSettings.General.QueueLimit;
    });

    // Authentication için daha sıkı rate limiting
    options.AddFixedWindowLimiter("Authentication", limiterOptions =>
    {
        limiterOptions.PermitLimit = rateLimitingSettings.Authentication.PermitLimit;
        limiterOptions.Window = rateLimitingSettings.Authentication.Window;
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = rateLimitingSettings.Authentication.QueueLimit;
    });

    // Asset operasyonları için özel rate limiting
    options.AddFixedWindowLimiter("AssetOperations", limiterOptions =>
    {
        limiterOptions.PermitLimit = rateLimitingSettings.AssetOperations.PermitLimit;
        limiterOptions.Window = rateLimitingSettings.AssetOperations.Window;
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = rateLimitingSettings.AssetOperations.QueueLimit;
    });

    // Rate limit aşıldığında döndürülecek response
    options.RejectionStatusCode = 429; // Too Many Requests
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        await context.HttpContext.Response.WriteAsync("Rate limit exceeded. Please try again later.", token);
    };
});
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<Application.Models.JwtSettings>() ?? new Application.Models.JwtSettings();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = ClaimTypes.Role,
            NameClaimType = ClaimTypes.Name
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials() 
                .SetIsOriginAllowed(origin => true); 
        });
});


var app = builder.Build();

// Log application startup
Log.Information("Starting up SolfixEnvanter Web API");

// Swagger UI sadece development ortamında aktif olur
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

// Rate Limiting Middleware
app.UseRateLimiter();

app.UseHttpsRedirection();

// HTTP request logging (Serilog) - Disabled
// app.UseSerilogRequestLogging();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

// Enrich logs with user/context info
app.Use(async (context, next) =>
{
    var userId = string.Empty;
    var userName = string.Empty;
    
    // JWT token'dan user bilgilerini çıkar
    if (context.User?.Identity?.IsAuthenticated == true)
    {
        // Önce NameIdentifier claim'ini dene
        userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        // Eğer yoksa "sub" claim'ini dene (JWT standard)
        if (string.IsNullOrEmpty(userId))
        {
            userId = context.User?.FindFirst("sub")?.Value;
        }
        
        // Eğer hala yoksa "userId" claim'ini dene
        if (string.IsNullOrEmpty(userId))
        {
            userId = context.User?.FindFirst("userId")?.Value;
        }
        
        // Username'i al
        userName = context.User?.Identity?.Name ?? 
                   context.User?.FindFirst(ClaimTypes.Name)?.Value ??
                   context.User?.FindFirst("username")?.Value ??
                   string.Empty;
    }
    
    var path = context.Request?.Path.Value ?? string.Empty;
    var ip = context.Connection?.RemoteIpAddress?.ToString() ?? string.Empty;
    var method = context.Request?.Method ?? string.Empty;

    using (LogContext.PushProperty("UserId", userId))
    using (LogContext.PushProperty("UserName", userName))
    using (LogContext.PushProperty("RequestPath", path))
    using (LogContext.PushProperty("RequestMethod", method))
    using (LogContext.PushProperty("ClientIp", ip))
    {
        await next();
    }
});

app.MapControllers();

try
{
    Log.Information("Starting SolfixEnvanter Web API");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}