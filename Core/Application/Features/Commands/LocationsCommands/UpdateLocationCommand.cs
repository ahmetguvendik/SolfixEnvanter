using MediatR;

namespace Application.Features.Commands;

public class UpdateLocationCommand : IRequest
{
    public string Id { get; set; }
    public string Name { get; set; } 
}
