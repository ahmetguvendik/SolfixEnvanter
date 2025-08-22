namespace Application.Features.Results.ProcedureResults;

public class GetAllProcedureQueryResult
{
    public string Id { get; set; }
    public string ProcedureName { get; set; }
    public string? ProcedureDescription { get; set; }
    public string? ProcedureFilePath { get; set; }
    public DateTime UploadedTime { get; set; }
}
