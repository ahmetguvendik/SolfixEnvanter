namespace Application.Features.Results.AssignmentFormResults;

public class GetAllAssignmentFormQueryResult
{
    public string Id { get; set; }
    public string AssignmentFormName { get; set; }
    public string? AssignmentFormDescription { get; set; }
    public string AssignmentFormFilePath { get; set; }
    public DateTime UploadedTime { get; set; }
}
