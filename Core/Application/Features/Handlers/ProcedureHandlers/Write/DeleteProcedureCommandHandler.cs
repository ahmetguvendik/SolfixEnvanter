using Application.Features.Commands.ProcedureCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Features.Handlers.ProcedureHandlers.Write;

public class DeleteProcedureCommandHandler : IRequestHandler<DeleteProcedureCommand>
{
    private readonly IGenericRepository<Procedure> _repository;
    private readonly IHostEnvironment _hostEnvironment;

    public DeleteProcedureCommandHandler(IGenericRepository<Procedure> repository, IHostEnvironment hostEnvironment)
    {
        _repository = repository;
        _hostEnvironment = hostEnvironment;
    }

    public async Task Handle(DeleteProcedureCommand request, CancellationToken cancellationToken)
    {
        var procedure = await _repository.GetByIdAsync(request.Id);
        
        if (procedure == null)
        {
            throw new Exception("Procedure not found.");
        }

        // Delete the associated PDF file
        if (!string.IsNullOrEmpty(procedure.ProcedureFilePath))
        {
            var filePath = procedure.ProcedureFilePath.Replace("/uploads/procedures/", "");
            var fullPath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "uploads", "procedures", filePath);
            
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        await _repository.RemoveAsync(procedure);
        await _repository.SaveChangesAsync();
    }
}
