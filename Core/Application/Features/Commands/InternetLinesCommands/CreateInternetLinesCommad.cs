using MediatR;

namespace Application.Features.Commands.InternetLinesCommands;

public class CreateInternetLinesCommad : IRequest
{
    public string LineNumber { get; set; }
    public string Provider { get; set; }
    public string Speed { get; set; }
    public DateTime ContractEndDate { get; set; }
    public string LocationId { get; set; }
}