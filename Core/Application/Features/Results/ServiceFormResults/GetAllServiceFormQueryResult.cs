namespace Application.Features.Results.ServiceFormResults;

public class GetAllServiceFormQueryResult
{
    public string Id { get; set; }
    public string ServiceFormName { get; set; }
    public string? ServiceFormDescription { get; set; }
    public string ServiceFormFilePath { get; set; }
    public DateTime UploadedTime { get; set; }
}
