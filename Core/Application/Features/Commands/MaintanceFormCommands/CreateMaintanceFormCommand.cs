using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Commands.MaintanceFormCommands;

public class CreateMaintanceFormCommand : IRequest
{
    public string FormName { get; set; }
    public string? FormDescription { get; set; }
    public DateTime UploadedTime { get; set; }
    public IFormFile FormFile { get; set; }
}