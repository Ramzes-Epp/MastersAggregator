using Dapper;
using MastersAggregatorService.Interfaces;
using MastersAggregatorService.Models;
using Npgsql;

namespace MastersAggregatorService.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }

    /// <summary>
    /// Получить список всех User (Async)
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        const string sqlQuery = @"SELECT id AS Id, name AS Name, first_name AS FirstName, pfone AS Pfone " +
                                @"FROM master_shema.users";
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var users = await connection.QueryAsync<User>(sqlQuery);
        
        return users;
    }


    /// <summary>
    /// Получить список всех User  
    /// </summary>
    /// <returns></returns> 
    public async Task<User> GetByIdAsync(int userId)
    {
        const string sqlQuery = @"SELECT id AS Id, name AS Name, first_name AS FirstName, pfone AS Pfone  " +
                                @"FROM master_shema.users WHERE Id = @userId";
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
         
        return await connection.QueryFirstOrDefaultAsync<User>(sqlQuery, new { userId });
    }
    public IEnumerable<User> GetAll()
    {
        return GetAllAsync().GetAwaiter().GetResult();
    }


    /// <summary>
    /// Получить обьект User по его id  
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns> 
    public User? GetById(int id)
    {
        return GetByIdAsync(id).Result;
    }

    public async Task<User> SaveAsync(User model)
    {
        const string sqlQuery = @"INSERT INTO master_shema.users (name, first_name, pfone) VALUES (@Name, @FirstName, @Pfone) RETURNING id";
         
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var id = await connection.ExecuteAsync(sqlQuery, new { model.Name, model.FirstName, model.Pfone }); 
        
        return new User { Id = (int)id, Name = model.Name, Pfone = model.Pfone, FirstName = model.FirstName}; 
    }
  
    public User Save(User model)
    {
        return SaveAsync(model).Result;
    }


    /// <summary>
    /// Удалить из БД User (Async)
    /// </summary>
    /// <param name="model"></param>
    public async Task DeleteAsync(User model)
    {
        const string sqlQuery = @"DELETE FROM master_shema.users WHERE id = @Id";
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        await connection.ExecuteAsync(sqlQuery, new { model.Id });
    }
     
    public void Delete(User model)
    {
        DeleteAsync(model).GetAwaiter().GetResult();
    }


    /// <summary>
    /// Обновить в БД User (Async)
    /// </summary>
    /// <param name="model"></param>
    public async Task UpdateAsync(User model)
    {
        const string sqlQuery = @"UPDATE master_shema.users SET name = @Name, first_name = @FirstName, pfone = @Pfone WHERE id = @Id";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        await connection.ExecuteAsync(sqlQuery, new { model.Id, model.Name, model.FirstName, model.Pfone });
    }  
}