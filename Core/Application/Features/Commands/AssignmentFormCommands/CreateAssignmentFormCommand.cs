using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Commands.AssignmentFormCommands;

public class CreateAssignmentFormCommand : IRequest
{
    public string AssignmentFormName { get; set; }
    public string? AssignmentFormDescription { get; set; }
    public IFormFile FormFile { get; set; }
    public DateTime UploadedTime { get; set; }
}
