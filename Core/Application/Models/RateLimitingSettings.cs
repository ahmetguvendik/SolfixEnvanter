namespace Application.Models;

public class RateLimitingSettings
{
    public RateLimitPolicy General { get; set; } = new();
    public RateLimitPolicy Authentication { get; set; } = new();
    public RateLimitPolicy AssetOperations { get; set; } = new();
}

public class RateLimitPolicy
{
    public int PermitLimit { get; set; } = 100;
    public TimeSpan Window { get; set; } = TimeSpan.FromMinutes(1);
    public int SegmentsPerWindow { get; set; } = 10;
    public int QueueLimit { get; set; } = 10;
}
