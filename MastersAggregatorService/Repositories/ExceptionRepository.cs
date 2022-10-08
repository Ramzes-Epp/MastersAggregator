using Dapper;
using MastersAggregatorService.Errors;
using MastersAggregatorService.Interfaces;
using Npgsql;

namespace MastersAggregatorService.Repositories;

public class ExceptionRepository : BaseRepository<ApiException>, IExceptionRepository
{
    public ExceptionRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<IEnumerable<ApiException>> GetAllAsync()
    { 
        const string sqlQuery = @"SELECT status_code AS StatusCode, user_name AS UserName, message AS Message, details AS Details " +
                                @"FROM master_shema.errors";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var exceptions = await connection.QueryAsync<ApiException>(sqlQuery);

        return exceptions;
    }
    
    public async Task SaveAsync(ApiException ex)
    { 
        const string sqlQuery = @"INSERT INTO master_shema.errors (status_code, user_name, message, details)" +
                                $@"VALUES (@{nameof(ex.StatusCode)}), (@{nameof(ex.UserName)}), (@{nameof(ex.Message)}), (@{nameof(ex.Details)})";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        await connection.ExecuteAsync(sqlQuery, ex);
    } 
}