using Dapper;
using MastersAggregatorService.Interfaces;
using MastersAggregatorService.Models;
using Npgsql;

namespace MastersAggregatorService.Repositories;


public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(IConfiguration configuration) : base(configuration)
    {
    }

    /// <summary>
    /// Получить список всех Order (Async)
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var orders = new List<Order>();//список всех Order 
        const string sqlQueryUserSenderImage = @"SELECT orders.id AS ordersid, users.id AS usersid, users.name AS name, users.first_name, users.pfone, images.url, images.description, images.id AS imagesid " +
                                                        @"FROM master_shema.orders " +
                                                        @"LEFT JOIN master_shema.users ON users.id = orders.user_id " +
                                                        @"LEFT JOIN master_shema.images ON images.order_id = orders.id " +
                                                        @"ORDER BY orders.id";
         
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        IEnumerable<dynamic> resultDynamicQueryUserSenderImage = connection.Query(sqlQueryUserSenderImage);

        var orderGroups = resultDynamicQueryUserSenderImage.GroupBy(r => r.ordersid);//группируем по id order

        foreach (var objOrder in orderGroups) 
        {
            //собираем обьект Images с orderGroups если есть Image добавляем в коллекцию
            var Images = new List<Image>();
            if (objOrder.First().imagesid != null)
            {
                foreach (var objImages in objOrder)  
                { 
                    Images.Add(new Image { Id = objImages.imagesid, ImageDescription = objImages.description, ImageUrl = objImages.url });
                }
            }
            
            //Собираем обьект Order с orderGroups
            Order newOrder = new Order
            {
                Id = objOrder.First().ordersid,
                Sender = new User { Id = objOrder.First().usersid, Name = objOrder.First().name, FirstName = objOrder.First().first_name, Pfone = objOrder.First().pfone },
                Images = Images
            };
             
            orders.Add(newOrder);
        } 

        return orders;
    }

    /// <summary>
    /// Получить список всех Order  
    /// </summary>
    /// <returns></returns>   
    public IEnumerable<Order> GetAll()
    {
        return GetAllAsync().GetAwaiter().GetResult();
    }

     
    /// <summary>
    /// Получить обьект Order по его id (Async)
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns> 
    public async Task<Order> GetByIdAsync(int idOrder)
    {
        const string sqlQueryUserSenderImage = @"SELECT orders.id AS ordersid, users.id AS usersid, users.name AS name, users.first_name, users.pfone, images.url, images.description, images.id AS imagesid " +
                                                        @"FROM master_shema.orders " +
                                                        @"LEFT JOIN master_shema.users ON users.id = orders.user_id " +
                                                        @"LEFT JOIN master_shema.images ON images.order_id = orders.id " +
                                                        @"WHERE orders.id = @idOrder";
         
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
           
        IEnumerable<dynamic> resultDynamicQueryUserSenderImage = connection.Query(sqlQueryUserSenderImage, new { idOrder });
         
        var orderGroup = resultDynamicQueryUserSenderImage.GroupBy(r => r.ordersid);//группируем по id order

        foreach (var objOrder in orderGroup)
        {
            //собираем обьект Images с orderGroups если есть Image добавляем в коллекцию
            var Images = new List<Image>();
            if (objOrder.First().imagesid != null)
            {
                foreach (var objImages in objOrder)
                {
                    Images.Add(new Image { Id = objImages.imagesid, ImageDescription = objImages.description, ImageUrl = objImages.url });
                }
            }

            //Собираем обьект Order с orderGroups
            Order newOrder = new Order
            {
                Id = objOrder.First().ordersid,
                Sender = new User { Id = objOrder.First().usersid, Name = objOrder.First().name, FirstName = objOrder.First().first_name, Pfone = objOrder.First().pfone },
                Images = Images
            };

            return newOrder;
        }

        return null; 
    }

    /// <summary>
    /// Получить обьект Order по его id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns> 
    public Order GetById(int id)
    {
        return GetByIdAsync(id).GetAwaiter().GetResult();
    }
     

    /// <summary>
    /// Добавить новый Order (Async)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<Order> SaveAsync(Order model)
    {
        //SQL запрос - добавляем в таблицу orders новый ордер если user_id существует
        string sqlQueryAddOrders = @"INSERT INTO master_shema.orders (user_id) " +
                                          @"SELECT users.id FROM master_shema.users " +
                                          @"WHERE master_shema.users.id = @Id " +
                                          @"RETURNING id";

        //SQL запрос - изменяем в таблице images номер order_id если существуют в таблице эта image и ее order_id IS NULL
        string sqlQueryAddImages = @"UPDATE master_shema.images  SET order_id = @resIdNewOrder  WHERE images.id = @Id AND order_id IS NULL";
         
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
         
        int? resIdNewOrder = await connection.QuerySingleAsync<int>(sqlQueryAddOrders, new { model.Sender.Id });
        if (resIdNewOrder == null)
            return null;

        //перебераем список Images и добавляем в БД master_shema.image_of_orders Id-картинки (добавляем если они существуют в master_shema.images)
        foreach (var image in model.Images)
        { 
            try
            {
                await connection.ExecuteAsync(sqlQueryAddImages, new { resIdNewOrder, image.Id });
            }
            catch (Exception)
            { 
            } 
        } 

        return model; 
    }

    /// <summary>
    /// Добавить новый Order  
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Order Save(Order model)
    {
        return SaveAsync(model).GetAwaiter().GetResult();
    }


    /// <summary>
    /// Удалить из БД Order (Async)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task DeleteAsync(Order model)
    {
        //В таблице master_shema.images в столбце order_id пишем NULL таким оброзом освобождаем картинку для добавления в другой ордер
        string sqlQuery = @"UPDATE master_shema.images SET order_id = NULL  WHERE images.order_id = @Id; " +
                          @"DELETE FROM master_shema.orders WHERE orders.id = @Id";
         
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        await connection.ExecuteAsync(sqlQuery, new { model.Id });
    }

    /// <summary>
    /// Удалить из БД Order
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public void Delete(Order model)
    {
        DeleteAsync(model).GetAwaiter().GetResult();
    } 
}
