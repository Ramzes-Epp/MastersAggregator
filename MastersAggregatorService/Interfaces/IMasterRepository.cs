using MastersAggregatorService.Models;

namespace MastersAggregatorService.Interfaces;

public interface IMasterRepository
{
    IEnumerable<Master> GetAll();
    Task<IEnumerable<Master>> GetAllAsync();
    public Master? GetById(int id);
    Task<Master> GetByIdAsync(int Id);
     
    public Master? Save(Master model);
    Task<Master> SaveAsync(Master model);
    public void Delete(Master model);
    Task DeleteAsync(Master model);
    Task<Master> UpdateAsync(Master model);
    Task<IEnumerable<Master>> GetByConditionAsync(bool condition);

}
