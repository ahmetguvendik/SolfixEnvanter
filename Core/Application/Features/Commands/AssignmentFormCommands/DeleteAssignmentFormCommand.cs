using MediatR;

namespace Application.Features.Commands.AssignmentFormCommands;

public class DeleteAssignmentFormCommand : IRequest
{
    public string Id { get; set; }
}
