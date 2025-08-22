using MediatR;

namespace Application.Features.Commands.MaintanceFormCommands;

public class DeleteMaintanceFormCommand : IRequest
{
    public string Id { get; set; }
}
