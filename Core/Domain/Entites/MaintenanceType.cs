using Domain.Entites;

public class MaintenanceType : BaseEntity
{
    public string Name { get; set; }               // Örn: Haftalık Bakım Formu
    public MaintenancePeriod Period { get; set; }  // Enum: Daily, Weekly, Monthly...
    public DateTime StartDate { get; set; }        // Başlangıç tarihi
    public ICollection<MaintenanceRecord> Records { get; set; } // Geçmiş bakımlar

}