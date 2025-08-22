using Application.Features.Commands.ServiceFormCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Features.Handlers.ServiceFormHandlers.Write;

public class DeleteServiceFormCommandHandler : IRequestHandler<DeleteServiceFormCommand>
{
    private readonly IGenericRepository<ServiceForm> _repository;
    private readonly IHostEnvironment _hostEnvironment;

    public DeleteServiceFormCommandHandler(IGenericRepository<ServiceForm> repository, IHostEnvironment hostEnvironment)
    {
        _repository = repository;
        _hostEnvironment = hostEnvironment;
    }

    public async Task Handle(DeleteServiceFormCommand request, CancellationToken cancellationToken)
    {
        var serviceForm = await _repository.GetByIdAsync(request.Id);
        
        if (serviceForm == null)
        {
            throw new Exception("Service form not found.");
        }

        // Delete the associated PDF file
        if (!string.IsNullOrEmpty(serviceForm.ServiceFormFilePath))
        {
            var filePath = serviceForm.ServiceFormFilePath.Replace("/uploads/services/", "");
            var fullPath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "uploads", "services", filePath);
            
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        await _repository.RemoveAsync(serviceForm);
        await _repository.SaveChangesAsync();
    }
}
