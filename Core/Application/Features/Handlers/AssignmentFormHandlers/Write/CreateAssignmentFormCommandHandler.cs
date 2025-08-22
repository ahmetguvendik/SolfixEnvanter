using Application.Features.Commands.AssignmentFormCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Features.Handlers.AssignmentFormHandlers.Write;

public class CreateAssignmentFormCommandHandler : IRequestHandler<CreateAssignmentFormCommand>
{
    private readonly IGenericRepository<AssignmentForm> _repository;
    private readonly IHostEnvironment _hostEnvironment;

    public CreateAssignmentFormCommandHandler(IGenericRepository<AssignmentForm> repository, IHostEnvironment hostEnvironment)
    {
        _repository = repository;
        _hostEnvironment = hostEnvironment;
    }

    public async Task Handle(CreateAssignmentFormCommand request, CancellationToken cancellationToken)
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

        var uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "uploads", "assignments");
        Directory.CreateDirectory(uploadsFolder);
        var fileName = Guid.NewGuid().ToString() + extension;
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.FormFile.CopyToAsync(stream);
        }

        var assignmentForm = new AssignmentForm
        {
            Id = Guid.NewGuid().ToString(),
            AssignmentFormName = request.AssignmentFormName,
            AssignmentFormDescription = request.AssignmentFormDescription,
            AssignmentFormFilePath = "/uploads/assignments/" + fileName,
            UploadedTime = request.UploadedTime
        };

        await _repository.CreateAsync(assignmentForm);
        await _repository.SaveChangesAsync();
    }
}
