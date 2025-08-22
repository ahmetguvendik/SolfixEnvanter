namespace Domain.Entities;

public class AssignmentForm : BaseEntity
{
    public string AssignmentFormName { get; set; }
    public string? AssignmentFormDescription { get; set; }
    public string AssignmentFormFilePath { get; set; }
    public DateTime UploadedTime { get; set; }
}
