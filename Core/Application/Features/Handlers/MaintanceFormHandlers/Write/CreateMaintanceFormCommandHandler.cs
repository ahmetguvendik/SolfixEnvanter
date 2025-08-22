using Application.Features.Commands.MaintanceFormCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Features.Handlers.MaintanceFormHandlers.Write;

public class CreateMaintanceFormCommandHandler : IRequestHandler<CreateMaintanceFormCommand>
{
    private readonly IGenericRepository<MaintanceForm> _repository;
    private readonly IHostEnvironment _hostEnvironment;

    public CreateMaintanceFormCommandHandler(IGenericRepository<MaintanceForm> faultReportRepository, IHostEnvironment hostEnvironment)
    {
        _repository = faultReportRepository;
        _hostEnvironment = hostEnvironment;
    }

    public async Task Handle(CreateMaintanceFormCommand request, CancellationToken cancellationToken)
    {
        string fileName = null;
        if (request.FormFile != null && request.FormFile.Length > 0)
        {
            var extension = Path.GetExtension(request.FormFile.FileName).ToLowerInvariant();
            var allowedExtensions = new[] { ".pdf" };

            if (!allowedExtensions.Contains(extension))
            {
                throw new Exception("Sadece PDF dosyalarına izin verilmektedir.");
            }

            var uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "uploads", "forms");
            Directory.CreateDirectory(uploadsFolder);
            fileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.FormFile.CopyToAsync(stream);
            }
        }

        var maintanceForm = new MaintanceForm
        {
            Id = Guid.NewGuid().ToString(),
            FormName = request.FormName,
            FormDescription = request.FormDescription,
            FormFilePath = fileName != null ? "/uploads/forms/" + fileName : "Belge Yok",
            UploadedTime = DateTime.Now
        };

        await _repository.CreateAsync(maintanceForm, cancellationToken);  
        await _repository.SaveChangesAsync(cancellationToken);
    }
}