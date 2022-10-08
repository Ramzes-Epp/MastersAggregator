using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public interface IImageRepository
{
    IEnumerable<Image> GetAll();
    Task<IEnumerable<Image>> GetAllAsync();
    public Image? GetById(int id);
    Task<Image> GetByIdAsync(int imageId);
    public Image? Save(Image model);
    Task<Image> SaveAsync(Image model);
    public void Delete(Image model);
    Task DeleteAsync(Image model);
    public void Update(Image model);
    Task UpdateAsync(Image model);
}