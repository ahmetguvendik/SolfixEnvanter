namespace Domain.Entities;

public class ServiceForm : BaseEntity
{
    public string ServiceFormName { get; set; }
    public string? ServiceFormDescription { get; set; }
    public string ServiceFormFilePath { get; set; }
    public DateTime UploadedTime { get; set; }
}
