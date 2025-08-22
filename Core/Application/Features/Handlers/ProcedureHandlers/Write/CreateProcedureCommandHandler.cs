using Application.Features.Commands.ProcedureCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Features.Handlers.ProcedureHandlers.Write;

public class CreateProcedureCommandHandler : IRequestHandler<CreateProcedureCommand>
{
    private readonly IGenericRepository<Procedure> _repository;
    private readonly IHostEnvironment _hostEnvironment;

    public CreateProcedureCommandHandler(IGenericRepository<Procedure> repository, IHostEnvironment hostEnvironment)
    {
        _repository = repository;
        _hostEnvironment = hostEnvironment;
    }

    public async Task Handle(CreateProcedureCommand request, CancellationToken cancellationToken)
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

                    var uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "uploads", "procedures");
            Directory.CreateDirectory(uploadsFolder);
            var fileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.FormFile.CopyToAsync(stream);
        }

        var procedure = new Procedure
        {
            Id = Guid.NewGuid().ToString(),
            ProcedureName = request.ProcedureName,
            ProcedureDescription = request.ProcedureDescription,
            ProcedureFilePath = "/uploads/procedures/" + fileName,
            UploadedTime = request.UploadedTime
        };

        await _repository.CreateAsync(procedure);
        await _repository.SaveChangesAsync();
    }
}
