namespace Application.Features.Results.InternetLinesResults;

public class GetAllInternetLinesQueryResult
{
    public string Id { get; set; }
    public string LineNumber { get; set; }
    public string Provider { get; set; }
    public string Speed { get; set; }
    public DateTime ContractEndDate { get; set; }
    public string LocationName { get; set; }

}