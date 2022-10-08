using Dapper;
using MastersAggregatorService.Models;
using Npgsql;

namespace MastersAggregatorService.Repositories;

public class ImageRepository : BaseRepository<Image>, IImageRepository
{
    public ImageRepository(IConfiguration configuration) : base(configuration)
    {
    }
    /// <summary>
    /// Вернуть список Image (Async)
    /// </summary>
    public async Task<IEnumerable<Image>> GetAllAsync()
    {
        const string sqlQuery = @"SELECT id AS Id, url AS ImageUrl, description AS ImageDescription FROM master_shema.images";
       
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        return await connection.QueryAsync<Image>(sqlQuery); 
    }

    public IEnumerable<Image> GetAll()
    {
        return GetAllAsync().Result;
    }


    /// <summary>
    /// Вернуть Image по id (Async)
    /// </summary> 
    public async Task<Image> GetByIdAsync(int imageId)
    {
        const string sqlQuery = @"SELECT id AS Id url AS ImageUrl description AS ImageDescription" +
                                @" FROM master_shema.images WHERE id = @Id";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        return await connection.QueryFirstAsync<Image>(sqlQuery, new { Id = imageId }); 
    }

    public Image GetById(int id)
    {
        return GetByIdAsync(id).Result;
    }


    /// <summary>
    /// Вернуть список Image (async method)
    /// </summary>  
    public async Task<Image> SaveAsync(Image model)
    {
        const string sqlQuery = @"INSERT INTO master_shema.images(url, description) VALUES (ImageUrl, ImageDescription) RETURNING id";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var id = connection.Query<int>(sqlQuery, new { ImageUrl = model.ImageUrl, ImageDescription = model.ImageDescription });

        return new Image {Id = id.FirstOrDefault(), ImageUrl = model.ImageUrl, ImageDescription = model.ImageDescription}; 
    }

    public Image Save(Image model)
    {
        return SaveAsync(model).Result;
    }


    /// <summary>
    /// Удалить Image из БД (async)
    /// </summary>
    public async Task DeleteAsync(Image model)
    {
        const string sqlQuery = "DELETE FROM master_shema.images WHERE id = @Id";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        await connection.ExecuteAsync(sqlQuery, new {Id = model.Id});
    }

    public void Delete(Image model)
    {
         DeleteAsync(model).GetAwaiter().GetResult();
    }


    /// <summary>
    /// Изменить в БД Image (async)
    /// </summary>
    public async Task UpdateAsync(Image model)
    {
        const string sqlQuery =
            @"UPDATE master_shema.images SET url = ImageUrl, description = ImageDescription " +
            $@"WHERE id = Id";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        await connection.ExecuteAsync(sqlQuery, new { Id = model.Id });
    }

    public void Update(Image model)
    {
        UpdateAsync(model);
    }
     
}