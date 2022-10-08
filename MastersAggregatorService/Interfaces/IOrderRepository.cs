using MastersAggregatorService.Models;

namespace MastersAggregatorService.Interfaces;

public interface IOrderRepository
{
    IEnumerable<Order> GetAll();
    Task<IEnumerable<Order>> GetAllAsync();
    public Order? GetById(int id);
    Task<Order> GetByIdAsync(int orderId);
     
    public Order? Save(Order model);
    Task<Order> SaveAsync(Order model);
    public void Delete(Order model);
    Task DeleteAsync(Order model);
}