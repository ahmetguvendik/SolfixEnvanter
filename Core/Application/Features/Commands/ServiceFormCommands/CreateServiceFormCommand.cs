using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Commands.ServiceFormCommands;

public class CreateServiceFormCommand : IRequest
{
    public string ServiceFormName { get; set; }
    public string? ServiceFormDescription { get; set; }
    public IFormFile FormFile { get; set; }
    public DateTime UploadedTime { get; set; }
}
