using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Commands.ProcedureCommands;

public class CreateProcedureCommand : IRequest
{
    public string ProcedureName { get; set; }
    public string? ProcedureDescription { get; set; }
    public IFormFile FormFile { get; set; }
    public DateTime UploadedTime { get; set; }
}
