namespace Domain.Entities;

public class Procedure : BaseEntity
{
    public string ProcedureName { get; set; }
    public string? ProcedureDescription { get; set; }
    public string ProcedureFilePath { get; set; }
    public DateTime UploadedTime { get; set; }
}
