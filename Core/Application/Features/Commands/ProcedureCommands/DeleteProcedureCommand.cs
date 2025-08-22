using MediatR;

namespace Application.Features.Commands.ProcedureCommands;

public class DeleteProcedureCommand : IRequest
{
    public string Id { get; set; }
}
