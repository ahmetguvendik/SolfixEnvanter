using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entites;
using Domain.Entities;
using MediatR;

namespace Application.Features.Handlers.AppUsers.Write;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IGenericRepository<AppUser> _repository;

    public UpdateUserCommandHandler(IGenericRepository<AppUser> repository)
    {
         _repository = repository;
    }
    
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
            throw new Exception("User not found");
            
        user.UserName = request.UserName;
        user.Email = request.Email;
        user.FullName = request.FullName;
        user.DepartmentId = request.DepartmentId;
        
        await _repository.UpdateAsync(user, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}
