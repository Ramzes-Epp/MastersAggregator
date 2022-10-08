using Dapper;
using MastersAggregatorService.Interfaces;
using MastersAggregatorService.Models;
using Npgsql;

namespace MastersAggregatorService.Repositories;

public class MasterRepository : BaseRepository<Master>, IMasterRepository
{
    public MasterRepository(IConfiguration configuration) : base(configuration)
    {
    }

    /// <summary>
    /// Вернуть список Master (Async)
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Master>> GetAllAsync()
    {
        string sqlQuery = @"SELECT id AS Id, name AS MastersName, is_active AS IsActive " +
                                @"FROM master_shema.masters  ORDER BY id";

        await using var connection = new NpgsqlConnection(ConnectionString); 
        connection.Open();
        var masters = await connection.QueryAsync<Master>(sqlQuery); 
        return masters;
    }
     
    public IEnumerable<Master> GetAll()
    {
        return GetAllAsync().GetAwaiter().GetResult();
    }


    /// <summary>
    /// Вернуть Master по id (Async)
    /// </summary> 
    public async Task<Master> GetByIdAsync(int idMaster)
    {
        string sqlQuery = @"SELECT id AS Id, name AS MastersName, is_active AS IsActive  " +
                                @"FROM master_shema.masters WHERE Id = @idMaster";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        return await connection.QueryFirstOrDefaultAsync<Master>(sqlQuery, new { idMaster }); 
    }
     
    public Master? GetById(int id)
    {
        return GetByIdAsync(id).GetAwaiter().GetResult();
    }


    /// <summary>
    /// Вернуть список Master в зависимости от занятости (async method)
    /// </summary>  
    public async Task<IEnumerable<Master>> GetByConditionAsync(bool condition) 
    {
        string sqlQuery = @"SELECT id AS Id, name AS MastersName, is_active AS IsActive " +
                                @"FROM master_shema.masters WHERE is_active = @condition";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var masters = await connection.QueryAsync<Master>(sqlQuery, new { condition }); 
        return masters;
    }
    public IEnumerable<Master> GetByCondition(bool condition)
    {
        return GetByConditionAsync(condition).GetAwaiter().GetResult();
    }


    /// <summary>
    /// Изменить в БД Master (async)
    /// </summary>
    public async Task<Master> UpdateAsync(Master model)
    { 
        string sqlQuery = @"UPDATE master_shema.masters SET is_active = @IsActive, name = @MastersName  WHERE id = @Id";  
 
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        await connection.ExecuteAsync(sqlQuery, new { model.Id, model.MastersName, model.IsActive });
        return model;
    }
    public Master Update(Master model)
    {
        return UpdateAsync(model).GetAwaiter().GetResult();
    }


    /// <summary>
    /// добавить в БД Master (async)
    /// </summary>
    public async Task<Master> SaveAsync(Master model)
    {
        string sqlQuery = @"INSERT INTO master_shema.masters (name, is_active) VALUES (@MastersName, @IsActive)"; 

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
         
        await connection.ExecuteAsync(sqlQuery, new { model.MastersName, model.IsActive });
        return model; 
    }
     
    public Master Save(Master model)
    {
        return SaveAsync(model).GetAwaiter().GetResult();
    }


    /// <summary>
    /// Удалить Master из БД (async)
    /// </summary>
    public async Task DeleteAsync(Master model)
    {
        string sqlQuery = @"DELETE FROM master_shema.masters WHERE id = @Id";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
         
        await connection.ExecuteAsync(sqlQuery, new { model.Id });
    }
     
    public void Delete(Master model)
    { 
        DeleteAsync(model).GetAwaiter().GetResult();
    } 
}