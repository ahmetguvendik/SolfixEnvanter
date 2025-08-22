using Application.Features.Commands.ServiceFormCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Features.Handlers.ServiceFormHandlers.Write;

public class CreateServiceFormCommandHandler : IRequestHandler<CreateServiceFormCommand>
{
    private readonly IGenericRepository<ServiceForm> _repository;
    private readonly IHostEnvironment _hostEnvironment;

    public CreateServiceFormCommandHandler(IGenericRepository<ServiceForm> repository, IHostEnvironment hostEnvironment)
    {
        _repository = repository;
        _hostEnvironment = hostEnvironment;
    }

    public async Task Handle(CreateServiceFormCommand request, CancellationToken cancellationToken)
    {
        // PDF dosyası zorunlu
        if (request.FormFile == null || request.FormFile.Length == 0)
        {
            throw new Exception("PDF file is required.");
        }

        var extension = Path.GetExtension(request.FormFile.FileName).ToLowerInvariant();
        var allowedExtensions = new[] { ".pdf" };

        if (!allowedExtensions.Contains(extension))
        {
            throw new Exception("Only PDF files are allowed.");
        }

        var uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "uploads", "services");
        Directory.CreateDirectory(uploadsFolder);
        var fileName = Guid.NewGuid().ToString() + extension;
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.FormFile.CopyToAsync(stream);
        }

        var serviceForm = new ServiceForm
        {
            Id = Guid.NewGuid().ToString(),
            ServiceFormName = request.ServiceFormName,
            ServiceFormDescription = request.ServiceFormDescription,
            ServiceFormFilePath = "/uploads/services/" + fileName,
            UploadedTime = request.UploadedTime
        };

        await _repository.CreateAsync(serviceForm);
        await _repository.SaveChangesAsync();
    }
}
