using MastersAggregatorService.Controllers;
using MastersAggregatorService.Interfaces;
using MastersAggregatorService.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace MastersAggregator.Test.ControllersTest;


[TestFixture(TestOf = typeof(OrderController))]
public  class OrderControllerTest
{
    [Test]
    public async Task GetOrder_Ok_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<IOrderRepository>();
        repository.GetByIdAsync(15).Returns(StaticDataOrder.TestOrder1);
        var controller = new OrderController(repository);
        // Act
        var result = await controller.GetOrder(15);
        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }


    [Test]
    public async Task GetOrder_Bad_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<IOrderRepository>();
        repository.GetByIdAsync(0).ReturnsNullForAnyArgs(); 
        var controller = new OrderController(repository);
        // Act
        var result = await controller.GetOrder(76);
        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
     

    [Test]
    public async Task GetOrders_Ok_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<IOrderRepository>();
        repository.GetAllAsync().Returns(StaticDataOrder.Orders); 
        var controller = new OrderController(repository);
        // Act
        var resultGetUsers = await controller.GetOrders();
        var expectedGetUsers = StaticData.Users;
        // Assert
        Assert.That((resultGetUsers as ObjectResult).StatusCode, Is.EqualTo(200));
    }


    [Test]
    public async Task DeleteOrder_Ok_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<IOrderRepository>();
        repository.GetByIdAsync(15).Returns(StaticDataOrder.TestOrder1); 
        var controller = new OrderController(repository);
        // Act
        var resultDeleteUser = await controller.DeleteOrder(15);
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(204));
    }


    [Test]
    public async Task DeleteOrder_Bad_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<IOrderRepository>();
        repository.GetByIdAsync(15).Returns(StaticDataOrder.TestOrder1); 
        var controller = new OrderController(repository);
        // Act
        var resultDeleteUser = await controller.DeleteOrder(150);
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(400));
    }


    [Test]
    public async Task CreateOrder_Ok_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<IOrderRepository>();
        repository.SaveAsync(StaticDataOrder.TestOrder2).Returns(StaticDataOrder.TestOrder2);
        repository.GetAllAsync().Returns(StaticDataOrder.Orders);  
        var controller = new OrderController(repository);
        // Act
        var resultDeleteUser = await controller.CreateOrder(StaticDataOrder.TestOrder2);
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(204));
    }


    [Test]
    public async Task CreateOrder_Bad_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<IOrderRepository>();
        repository.GetAllAsync().Returns(StaticDataOrder.Orders); 
        var controller = new OrderController(repository);
        // Act
        var resultDeleteUser = await controller.CreateOrder(StaticDataOrder.TestOrder1);
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(400));
    }
}

public static class StaticDataOrder
{
    //TestOrder1 - существует в списке Orders
    public static Order TestOrder1 = new Order { Id = 1, Sender = new User { Id = 1, Name = "Антон", FirstName = "Быстрый", Pfone = "+745-77-88-111" }, Images = new List<Image> { new Image { Id = 1, ImageUrl = "https://my-domen.com/conten/images/21325.ipg", ImageDescription = "описание работы: не закрываеться окно на фото видно проблему" } } };
    //TestOrder2 - нет в списке Orders
    public static Order TestOrder2 = new Order { Id = 8, Sender = new User { Id = 1, Name = "Антон", FirstName = "Быстрый", Pfone = "+745-77-88-111" }, Images = new List<Image> { new Image { Id = 15, ImageUrl = "https://my-domen.com/conten/images/7777.ipg", ImageDescription = "описание работы: не закрываеться фото" } } };

    public static User Sender = new User
        { Id = 1, Name = "Антон", FirstName = "Быстрый", Pfone = "+745-77-88-111" };

    public static List<Order> Orders = new List<Order>
    {
        new Order {Id = 0, Sender = new User { Id = 0, Name = "Sergey", FirstName = "Sidorov", Pfone = "+745-34-34-153"}, Images = new List<Image> { new Image { Id = 3, ImageUrl = "https://my-domen.com/conten/images/27776.ipg", ImageDescription = "описание работы: перекос двери" }, new Image { Id = 2, ImageUrl = "https://my-domen.com/conten/images/21326.ipg", ImageDescription = "описание работы: перекос окна вид с другой стороны" } }},
        new Order {Id = 1, Sender = new User { Id = 1, Name = "Антон", FirstName = "Быстрый", Pfone = "+745-77-88-111" }, Images = new List<Image> { new Image { Id = 1, ImageUrl = "https://my-domen.com/conten/images/21325.ipg", ImageDescription = "описание работы: не закрываеться окно на фото видно проблему" } }},
        new Order {Id = 2, Sender = new User { Id = 2, Name = "Kolia", FirstName = "Smelov", Pfone = "+745-88-11-222" },  Images = new List<Image> { new Image { Id = 3, ImageUrl = "https://my-domen.com/conten/images/21224.ipg", ImageDescription = "описание работы: починить дверной замок - сломалс¤ ключ" }, new Image { Id = 4, ImageUrl = "https://my-domen.com/conten/images/2115.ipg", ImageDescription = "описание работы: на фото видно проблему" } }}
    }; 
}