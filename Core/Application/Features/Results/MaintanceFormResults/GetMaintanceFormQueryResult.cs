using System.Security.AccessControl;

namespace Application.Features.Results.MaintanceFormResults;

public class GetMaintanceFormQueryResult
{
    public string Id { get; set; }
    public string FormName { get; set; }
    public string? FormDescription { get; set; }
    public string FormFilePath { get; set; }
    public DateTime UploadedTime { get; set; }
    
}