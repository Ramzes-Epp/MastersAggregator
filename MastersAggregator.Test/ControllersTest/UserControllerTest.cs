using MastersAggregatorService.Controllers;
using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace MastersAggregator.Test.ControllersTest;

[TestFixture(TestOf = typeof(UserController))]
public class UserControllerTest
{
    [Test]
    public async Task GetByIdOkResultTest()
    {
        var repository = Substitute.For<IUserRepository>();
        repository.GetByIdAsync(15).Returns(Task.FromResult(StaticData.TestUser1));
        var controller = new UserController(repository);
        var result = await controller.GetUserById(15);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }


    [Test]
    public async Task GetByIdBadResultTest()
    {
        var repository = Substitute.For<IUserRepository>();
        repository.GetByIdAsync(0).ReturnsNullForAnyArgs();
        var controller = new UserController(repository);
        var result = await controller.GetUserById(76);
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }


    [Test]
    public async Task GetUsersOkResultTest()
    {
        // Arrange
        var repository = Substitute.For<IUserRepository>(); 
        repository.GetAllAsync().Returns(StaticData.Users);
        var controller = new UserController(repository); 
        // Act
        var resultGetUsers = await controller.GetAllUsers();  
        var expectedGetUsers = StaticData.Users;
        Assert.That((resultGetUsers as ObjectResult).StatusCode, Is.EqualTo(200));
        // Assert
        Assert.That((resultGetUsers as ObjectResult).Value, Is.EqualTo(expectedGetUsers));
    }


    [Test]
    public async Task DeleteUserOkResultTest()
    {
        // Arrange
        var repository = Substitute.For<IUserRepository>();
        repository.GetByIdAsync(15).Returns(Task.FromResult(StaticData.TestUser1)); 
        var controller = new UserController(repository);
        // Act
        var resultDeleteUser = await controller.DeleteUser(15); 
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(204));

    }
    

    [Test]
    public async Task DeleteUserBadResultTest()
    {
        // Arrange
        var repository = Substitute.For<IUserRepository>();
        repository.GetByIdAsync(150).Returns(Task.FromResult<User>(null));
        // Act
        var controller = new UserController(repository);
        var resultDeleteUser = await controller.DeleteUser(150);
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(404)); 

    }


    [Test]
    public async Task CreateUserOkResultTest()
    {
        // Arrange
        var repository = Substitute.For<IUserRepository>();
        repository.GetAllAsync().Returns(Task.FromResult<IEnumerable<User>>(StaticData.Users));
        var controller = new UserController(repository);
        // Act
        var resultDeleteUser = await controller.CreateUser(StaticData.TestUser1);
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(204));
    }


    [Test]
    public async Task CreateUserBadResultTest()
    {
        // Arrange
        var repository = Substitute.For<IUserRepository>();
        repository.GetAllAsync().Returns(StaticData.Users);
        var controller = new UserController(repository);
        // Act
        var resultDeleteUser = await controller.CreateUser(StaticData.TestUser2);
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(400));
    }


    [Test]
    public async Task UpdateUserOkResultTest()
    {
        // Arrange
        var repository = Substitute.For<IUserRepository>(); 
        repository.GetAllAsync().Returns(Task.FromResult<IEnumerable<User>>(StaticData.Users));
        var controller = new UserController(repository);
        // Act
        var resultDeleteUser = await controller.UpdateUser(StaticData.TestUser2);
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(204));
    }


    [Test]
    public async Task UpdateUserBadResultTest()
        // Arrange
    {
        var repository = Substitute.For<IUserRepository>(); 
        var controller = new UserController(repository);
        repository.GetAllAsync().Returns(Task.FromResult<IEnumerable<User>>(StaticData.Users));
        // Act
        var resultDeleteUser = await controller.UpdateUser(StaticData.TestUser1);
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(400));
    } 
}


static class StaticData
{
    //TestUser1 - уникальный юзер нет в Users
    //TestUser2 - есть в списке Users
    public static User TestUser1 = new User { Id = 15, Name = "Vadim", FirstName = "Sidor", Pfone = "+745-34-34-153" };
    public static User TestUser2 = new User { Id = 1, Name = "Sasha", FirstName = "Smelov", Pfone = "+745-88-11-222" };

    public static List<User> Users = new List<User>
    {
        new User { Id = 0, Name = "Andrey", FirstName = "Sidorov", Pfone = "+745-34-34-154" },
        new User { Id = 1, Name = "Sasha",  FirstName = "Smelov",  Pfone = "+745-88-11-222" }
    };

}