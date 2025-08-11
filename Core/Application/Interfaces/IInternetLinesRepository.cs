namespace Application.Interfaces;

public interface IInternetLinesRepository
{
    Task<List<InternetLine>> GetAllInternetLinesWithLocation();
}