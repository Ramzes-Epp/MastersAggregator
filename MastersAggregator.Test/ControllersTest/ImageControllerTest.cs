using MastersAggregatorService.Controllers;
using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;

namespace MastersAggregator.Test.ControllersTest;

public class ImageControllerTest
{
    [Test]
    public async Task Get_Image_Ok_Result_Test()
    {
        
        // Arrange
        var repository = Substitute.For<IImageRepository>();
        repository.GetByIdAsync(15).Returns(Task.FromResult(StaticDataImage.TestImage1));
        var controller = new ImageController(repository);
        // Act
        var result = await controller.GetImageById(15);
        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task Get_Image_Bad_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<IImageRepository>();
        repository.GetByIdAsync(15).Returns(Task.FromResult(StaticDataImage.TestImage1));
        var controller = new ImageController(repository);
        // Act
        var result = await controller.GetImageById(100);
        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

 
    [Test]
    public async Task Get_Images_Ok_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<IImageRepository>();
        repository.GetAllAsync().Returns(StaticDataImage.Images);
        var controller = new ImageController(repository);
        // Act
        var result = await controller.GetAllImages();
        var expectedGetUsers = StaticData.Users;
        // Assert
        Assert.That((result as ObjectResult).StatusCode, Is.EqualTo(200)); 
    }

    [Test]
    public async Task Delete_Image_Ok_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<IImageRepository>();
        repository.GetByIdAsync(15).Returns(Task.FromResult(StaticDataImage.TestImage1));
        var controller = new ImageController(repository);
        // Act
        var result = await controller.DeleteImage(15);
        // Assert
        Assert.That((result as StatusCodeResult).StatusCode, Is.EqualTo(204)); 
    }

    [Test]
    public async Task Delete_Image_Bad_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<IImageRepository>();
        repository.GetByIdAsync(15).Returns(Task.FromResult(StaticDataImage.TestImage1));
        var controller = new ImageController(repository);
        // Act
        var result = await controller.DeleteImage(150);
        // Assert
        Assert.That((result as StatusCodeResult).StatusCode, Is.EqualTo(404));
    }

    [Test]
    public async Task Create_Image_Ok_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<IImageRepository>();
        repository.GetAllAsync().Returns(Task.FromResult<IEnumerable<Image>>(StaticDataImage.Images));
        var controller = new ImageController(repository);
        // Act
        var result = await controller.CreateImage(StaticDataImage.TestImage1);
        // Assert
        Assert.That((result as StatusCodeResult).StatusCode, Is.EqualTo(204));
    }

    [Test]
    public async Task Create_User_Image_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<IImageRepository>();
        repository.GetAllAsync().Returns(Task.FromResult<IEnumerable<Image>>(StaticDataImage.Images));
        var controller = new ImageController(repository);
        // Act
        var result = await controller.CreateImage(StaticDataImage.TestImage2);
        // Assert
        Assert.That((result as StatusCodeResult).StatusCode, Is.EqualTo(400));
    }

    [Test]
    public async Task Update_Image_Ok_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<IImageRepository>();
        repository.GetAllAsync().Returns(Task.FromResult<IEnumerable<Image>>(StaticDataImage.Images));
        var controller = new ImageController(repository);
        // Act
        var result = await controller.UpdateImage(StaticDataImage.TestImage2);
        // Assert
        Assert.That((result as StatusCodeResult).StatusCode, Is.EqualTo(204));
    }

    [Test]
    public async Task Update_Image_Bad_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<IImageRepository>();
        repository.GetAllAsync().Returns(Task.FromResult<IEnumerable<Image>>(StaticDataImage.Images));
        var controller = new ImageController(repository);
        // Act
        var result = await controller.UpdateImage(StaticDataImage.TestImage1);
        // Assert
        Assert.That((result as StatusCodeResult).StatusCode, Is.EqualTo(400));
    }

}

 
public static class StaticDataImage
{
    //TestUser1 - уникальный юзер нет в Users
    public static Image TestImage1 = new Image { Id = 15, ImageUrl = "https://my-domen.com/conten/images/21515.ipg", ImageDescription = "описание работы: перекос крыши вид с перекосившегося окна" };
    //TestUser2 - есть в списке Users
    public static Image TestImage2 = new Image { Id = 2, ImageUrl = "https://my-domen.com/conten/images/21326.ipg", ImageDescription = "описание работы: перекос окна вид с другой стороны" };

    public static List<Image> Images = new List<Image>
        {
            new Image { Id = 0, ImageUrl = "https://my-domen.com/conten/images/21324.ipg", ImageDescription = "описание работы: необходимо починить дверной замок на фото показана поломка - сломался ключ" },
            new Image { Id = 1, ImageUrl = "https://my-domen.com/conten/images/21325.ipg", ImageDescription = "описание работы: у меня не закрываеться окно на фото видно проблему" },
            new Image { Id = 2, ImageUrl = "https://my-domen.com/conten/images/21326.ipg", ImageDescription = "описание работы: перекос окна вид с другой стороны" }
        };

}