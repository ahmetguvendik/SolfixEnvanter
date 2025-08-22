namespace Domain.Entities;

public class MaintanceForm : BaseEntity
{
    public string FormName { get; set; }
    public string? FormDescription { get; set; }
    public string FormFilePath { get; set; }
    public DateTime UploadedTime { get; set; }
}