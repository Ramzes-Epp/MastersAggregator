using MastersAggregatorService.Controllers;
using MastersAggregatorService.Interfaces;
using MastersAggregatorService.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace MastersAggregator.Test;

[TestFixture(TestOf = typeof(MasterController))]
public class MasterControllerTest
{
    [Test]
    public async Task GetAllTest()
    { 
        // Arrange
        var expectedJson = StaticData.ReturnExpectedActionResult() as JsonResult;
        var repository = Substitute.For<IMasterRepository>();
        repository.GetAllAsync().Returns(StaticData.Masters);
        var controller = new MasterController(repository);
        // Act
        var objectResultFromGetAllTest = await controller.GetAll();
        // Assert
        Assert.That(objectResultFromGetAllTest, Is.Not.Null); 
        Assert.That(objectResultFromGetAllTest, Is.InstanceOf<OkObjectResult>());
        Assert.That((objectResultFromGetAllTest as ObjectResult).Value, Is.EqualTo(expectedJson.Value));

    }

    [Test]
    public async Task GetByIdTest()
    {
        // Arrange
        var repository = Substitute.For<IMasterRepository>();  
        repository.GetByIdAsync(1).Returns(Task.FromResult(StaticData.testMaster));
        var controller = new MasterController(repository); 
        // Act
        var objectResultFromGetById = await controller.GetById(1);
        // Assert 
        Assert.That((objectResultFromGetById as ObjectResult).Value, Is.Not.Null);
        Assert.That(objectResultFromGetById, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task GetByConditionTest()
    {
        // Arrange
        var repository = Substitute.For<IMasterRepository>();
        var modelCondition = false;
        repository.GetByConditionAsync(modelCondition).Returns(StaticData.Masters.Where(x => x.IsActive == modelCondition));      
        var controller = new MasterController(repository);
        
        //Act
        var objectResultFromGetByCondition = await controller.GetByCondition(modelCondition) as ObjectResult; 
        var expectedJson = StaticData.ReturnExpectedActionResultByCondition(modelCondition) as JsonResult;
         
        //Assert
        Assert.That(objectResultFromGetByCondition, Is.Not.Null);        
        Assert.That(objectResultFromGetByCondition.StatusCode, Is.EqualTo(200));
        Assert.That(objectResultFromGetByCondition.Value, Is.EqualTo(expectedJson.Value));
    }

    [Test]
    public async Task UpdateMasterTest()
    {
        // Arrange
        var repository = Substitute.For<IMasterRepository>();   
        repository.GetAllAsync().Returns(StaticData.Masters);
        var controller = new MasterController(repository);
        //Act 
        var objectResultFromUpdateMaster = await controller.UpdateMaster(StaticData.testMaster);
        //Assert 
        Assert.That((objectResultFromUpdateMaster as StatusCodeResult).StatusCode, Is.EqualTo(204));
    }

    [Test]
    public async Task DeleteMastertTest()
    {
        // Arrange
        var repository = Substitute.For<IMasterRepository>();
        repository.GetByIdAsync(1).Returns(Task.FromResult(StaticData.testMaster));
        // Act
        var controller = new MasterController(repository);
        var resultDeleteUser = await controller.DeleteMaster(1);
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(204));

    }
}

public static class StaticData
{
    public static Master testMaster = new Master() { Id = 1, MastersName = "Name1", IsActive = false };
    
    public static IEnumerable<Master> Masters = new[]
    {
        new Master(){Id = 0, MastersName = "Name0", IsActive = true },
        new Master(){Id = 1, MastersName = "Name1", IsActive = false },
        new Master(){Id = 2, MastersName = "Name2", IsActive = true }
    };

    public static IActionResult ReturnExpectedActionResult()
    {
        return new JsonResult(Masters);
    }

    public static IActionResult ReturnExpectedActionResultById(int id)
    {
        return new JsonResult(Masters.FirstOrDefault(x => x.Id == id));
    }

    public static IActionResult ReturnExpectedActionResultByCondition(bool condition)
    {
        return new JsonResult(Masters.Where(x => x.IsActive == condition).ToList());
    }
}