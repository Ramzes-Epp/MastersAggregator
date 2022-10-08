using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    Task<IEnumerable<User>> GetAllAsync();
    public User? GetById(int id);
    Task<User> GetByIdAsync(int userId);
     
    public User? Save(User model);
    Task<User> SaveAsync(User model);
    public void Delete(User model);
    Task DeleteAsync(User model); 
    Task UpdateAsync(User model); 
}