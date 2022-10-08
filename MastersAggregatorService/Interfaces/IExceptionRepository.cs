using MastersAggregatorService.Errors;

namespace MastersAggregatorService.Interfaces;

public interface IExceptionRepository
{
    Task<IEnumerable<ApiException>> GetAllAsync();
    Task SaveAsync(ApiException model);
}