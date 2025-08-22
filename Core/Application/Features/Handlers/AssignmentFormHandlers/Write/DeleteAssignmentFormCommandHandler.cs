using Application.Features.Commands.AssignmentFormCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Features.Handlers.AssignmentFormHandlers.Write;

public class DeleteAssignmentFormCommandHandler : IRequestHandler<DeleteAssignmentFormCommand>
{
    private readonly IGenericRepository<AssignmentForm> _repository;
    private readonly IHostEnvironment _hostEnvironment;

    public DeleteAssignmentFormCommandHandler(IGenericRepository<AssignmentForm> repository, IHostEnvironment hostEnvironment)
    {
        _repository = repository;
        _hostEnvironment = hostEnvironment;
    }

    public async Task Handle(DeleteAssignmentFormCommand request, CancellationToken cancellationToken)
    {
        var assignmentForm = await _repository.GetByIdAsync(request.Id);
        
        if (assignmentForm == null)
        {
            throw new Exception("Assignment form not found.");
        }

        // Delete the associated PDF file
        if (!string.IsNullOrEmpty(assignmentForm.AssignmentFormFilePath))
        {
            var filePath = assignmentForm.AssignmentFormFilePath.Replace("/uploads/assignments/", "");
            var fullPath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "uploads", "assignments", filePath);
            
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        await _repository.RemoveAsync(assignmentForm);
        await _repository.SaveChangesAsync();
    }
}
