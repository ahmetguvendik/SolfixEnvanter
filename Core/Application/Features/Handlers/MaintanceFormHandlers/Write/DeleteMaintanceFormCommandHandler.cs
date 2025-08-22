using Application.Features.Commands.MaintanceFormCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Features.Handlers.MaintanceFormHandlers.Write;

public class DeleteMaintanceFormCommandHandler : IRequestHandler<DeleteMaintanceFormCommand>
{
    private readonly IGenericRepository<MaintanceForm> _repository;
    private readonly IHostEnvironment _hostEnvironment;

    public DeleteMaintanceFormCommandHandler(IGenericRepository<MaintanceForm> repository, IHostEnvironment hostEnvironment)
    {
        _repository = repository;
        _hostEnvironment = hostEnvironment;
    }

    public async Task Handle(DeleteMaintanceFormCommand request, CancellationToken cancellationToken)
    {
        var maintanceForm = await _repository.GetByIdAsync(request.Id);
        
        if (maintanceForm == null)
        {
            throw new Exception("Bakım formu bulunamadı.");
        }

        // Eğer dosya varsa, dosyayı da sil
        if (!string.IsNullOrEmpty(maintanceForm.FormFilePath) && maintanceForm.FormFilePath != "Belge Yok")
        {
            var filePath = maintanceForm.FormFilePath.Replace("/uploads/forms/", "");
            var fullPath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "uploads", "forms", filePath);
            
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        await _repository.RemoveAsync(maintanceForm);
        await _repository.SaveChangesAsync();
    }
}
