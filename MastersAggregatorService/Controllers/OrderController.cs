using MastersAggregatorService.Interfaces;
using MastersAggregatorService.Models;
using Microsoft.AspNetCore.Mvc;


namespace MastersAggregatorService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
public class OrderController : BaseController<Order>
{
    private readonly IOrderRepository _repository; 
    public OrderController(IOrderRepository repository) 
    {
        _repository = repository; 
    }
 
    
    /// <summary>
    /// GET all order
    /// </summary> 
    /// <returns>List of all Order</returns>  
    /// <response code="200"> Returns List of all Order.</response>
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _repository.GetAllAsync();
        if (orders.Any())
            return Ok(orders);
        else
            return NotFound();
    }


    /// <summary>
    /// GET by Id order
    /// </summary>
    /// <param name="id"> Id Order</param>
    /// <returns>Order by id</returns>  
    /// <response code="200"> Returns Order.</response>
    /// <response code="400"> Order id does not exist.</response>
    [HttpGet("id")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order is null)
            return NotFound();
        else
            return Ok(new JsonResult(order));
    }


    /// <summary>
    /// delete id order
    /// </summary> 
    /// <param name = "id"> Id Order </param>
    /// <returns>Delete Id Order</returns>  
    /// <response code="200"> successfully deleted Order.</response>
    /// <response code="400"> failed to delete order, such id order does not exist.</response>
    [HttpDelete("id")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order is null)
            return BadRequest();
        else
        {
            await _repository.DeleteAsync(order);
            return NoContent();
        } 
    }


    /// <summary>
    /// Add new Order. 
    /// </summary>
    /// <param name="order">order Model Order.</param> 
    /// <returns></returns>
    /// <response code="200"> create Order.</response>
    /// <response code="400"> I can't create an Order, such an Order already exists.</response> 
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {  
        var result = await _repository.SaveAsync(order);
        if (result == null)
            return BadRequest();

        return NoContent(); 
    }
}