using MediatR;

namespace Application.Features.Commands;

public class CreateLocationCommand : IRequest
{
    public string Name { get; set; } 
}