using Domain.Entites;

namespace Application.Interfaces;

public interface IInternetLinesRepository
{
    Task<List<InternetLine>> GetAllInternetLinesWithLocation();
    Task<InternetLine> GetInternetLineByIdWithLocation(string id);
}