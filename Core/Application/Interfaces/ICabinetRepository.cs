using Domain.Entites;

namespace Application.Interfaces;

public interface ICabinetRepository
{
    Task<IList<Cabinet>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Cabinet> GetByIdWithDetailsAsync(string id, CancellationToken cancellationToken = default);
}