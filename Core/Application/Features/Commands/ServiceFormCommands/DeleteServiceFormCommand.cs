using MediatR;

namespace Application.Features.Commands.ServiceFormCommands;

public class DeleteServiceFormCommand : IRequest
{
    public string Id { get; set; }
}
