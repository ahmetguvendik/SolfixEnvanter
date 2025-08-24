using MediatR;

namespace Application.Features.Commands;

public class UpdateCabinetCommand : IRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? Code { get; set; }
    public int? UHeight { get; set; }
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public string? SerialNumber { get; set; }
    public string? PowerFeed { get; set; }
    public int? MaxLoadKg { get; set; }
    public int? MaxPowerWatts { get; set; }
    public string? CoolingType { get; set; }
    public string? Notes { get; set; }
    public string LocationId { get; set; }
}
