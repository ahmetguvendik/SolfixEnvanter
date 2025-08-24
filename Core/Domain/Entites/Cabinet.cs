using Domain.Entities;

namespace Domain.Entites;

public class Cabinet : BaseEntity
{
    public string Name { get; set; } // Rack-01
    public string? Code { get; set; } // İç kod / envanter numarası
    public int? UHeight { get; set; } // 42U, 24U vb.
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public string? SerialNumber { get; set; }

    public string? PowerFeed { get; set; } // Tek faz, üç faz vb.
    public int? MaxLoadKg { get; set; }
    public int? MaxPowerWatts { get; set; }
    public string? CoolingType { get; set; }

    public string? Notes { get; set; }

    // Lokasyon ilişkisi
    public string LocationId { get; set; }
    public Location Location { get; set; }

    // İçindeki cihazlar
    public ICollection<Asset> Assets { get; set; }
}