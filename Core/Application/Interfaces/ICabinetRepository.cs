using Domain.Entites;

namespace Application.Interfaces;

public interface ICabinetRepository
{
    Task<IList<Cabinet>> GetAllAsync(CancellationToken cancellationToken = default); 
}