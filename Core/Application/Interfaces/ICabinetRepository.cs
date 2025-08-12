namespace Application.Interfaces;

public interface ICabinetRepository
{
    Task<IList<Cabinet>> GetAllAsync(); 
}