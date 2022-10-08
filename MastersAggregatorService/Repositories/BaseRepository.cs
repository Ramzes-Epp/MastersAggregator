using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public abstract class BaseRepository<T> where T : BaseModel
{
    private protected readonly string ConnectionString;
    protected BaseRepository(IConfiguration configuration)
    {
        ConnectionString = configuration.GetValue<string>("ConnectionString");
    }
}