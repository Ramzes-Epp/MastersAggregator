using Dapper;
using MastersAggregatorService.Models;
using Npgsql;

namespace MastersAggregatorService.Repositories;

public class TokenRepository : BaseRepository<Token>
{
    public TokenRepository(IConfiguration configuration) : base(configuration)
    {
    }

    /// <summary>
    /// Получаем все Api токены из БД
    /// </summary>
    /// <returns></returns> 
    public async Task<IEnumerable<Token>> GetAllApiKeyBdAsync()
    {
        //получаем список Token из master_shema.access
        string sqlQuery = @"SELECT api_token AS ApiToken, user_name AS ApiUserName  " +
                                 @"FROM master_shema.access";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        return await connection.QueryAsync<Token>(sqlQuery);
    }
 
}
