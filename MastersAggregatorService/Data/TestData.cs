using MastersAggregatorService.Models;
 
namespace MastersAggregatorService.Data;

//Класс в котором храним входные тестовые данные для проекта 
public static class TestData
{
    public static List<User> Users = new List<User>
    {
        new User { Id = 0, Name = "Sergey", FirstName = "Sidorov", Pfone = "+745-34-34-153" },
        new User { Id = 1, Name = "Антон", FirstName = "Быстрый", Pfone = "+745-77-88-111" },
        new User { Id = 5, Name = "Kolia", FirstName = "Smelov", Pfone = "+745-88-11-222" }
    };

    public static List<Image> Images = new List<Image>
    {
        new Image { Id = 0, ImageUrl = "https://my-domen.com/conten/images/21324.ipg", ImageDescription = "описание работы: необходимо починить дверной замок на фото показана поломка - сломался ключ" },
        new Image { Id = 1, ImageUrl = "https://my-domen.com/conten/images/21325.ipg", ImageDescription = "описание работы: у меня не закрываеться окно на фото видно проблему" },
        new Image { Id = 2, ImageUrl = "https://my-domen.com/conten/images/21326.ipg", ImageDescription = "описание работы: перекос окна вид с другой стороны" }
    };

    public static List<Order> Orders = new List<Order>
    {
        new Order { Id = 0, Sender = Users[0], Images = new List<Image> { Images[0] } },
        new Order { Id = 1, Sender = Users[1], Images = new List<Image> { Images[1], Images[2] } }
    }; 
}
 
