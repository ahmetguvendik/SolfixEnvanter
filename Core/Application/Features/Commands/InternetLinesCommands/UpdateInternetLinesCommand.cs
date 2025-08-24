using MediatR;

namespace Application.Features.Commands;

public class UpdateInternetLinesCommand : IRequest
{
    public string Id { get; set; }
    public string LineNumber { get; set; }
    public string Provider { get; set; }
    public string Speed { get; set; }
    public DateTime ContractEndDate { get; set; }
    public string LocationId { get; set; }
}
