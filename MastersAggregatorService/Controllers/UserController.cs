using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
public class UserController : BaseController<User>
{
    private readonly IUserRepository _repository;
    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// GET user by id
    /// </summary>
    /// <param name="User's Id"></param>
    /// <returns>User by id</returns>
    /// <response code="200"> Returns user by id in Json format.</response>
    /// <response code="404"> User does not exist.</response>
    [HttpGet]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user is null)
            return NotFound();
        else
            return Ok(new JsonResult(user));
    }

    /// <summary>
    /// GET all users
    /// </summary> 
    /// <returns>List of all users in Json format</returns>
    /// <response code="200"> Returns List of all users in Json format.</response>
    /// <response code="404"> Users not found.</response>
    [HttpGet] 
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _repository.GetAllAsync();
        if (users.Any())
            return Ok(users);
        else
            return NotFound();
    }

    /// <summary>
    /// Delete user by id
    /// </summary> 
    /// <returns></returns>
    /// <response code="204"> User deleted</response>
    /// <response code="404"> User not found</response>
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user is null)
            return NotFound();
        else
        {
            await _repository.DeleteAsync(user);
            return NoContent();
        }
    }
     

    /// <summary>
    /// POST to create user
    /// </summary> 
    /// <returns></returns>
    /// <response code="204"> User created</response>
    /// <response code="404"> Invalid model</response>
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody]User user)
    { 
        await _repository.SaveAsync(user);
        return NoContent(); 
    }
    
    /// <summary>
    /// PUT to update user
    /// </summary> 
    /// <returns></returns>
    /// <response code="204"> User updated</response>
    /// <response code="400"> Invalid model</response>
    [HttpPut]
    public async Task<IActionResult> UpdateUser( User user)
    {
        var users = await _repository.GetAllAsync();

        if (users.Any(u => u.Id.Equals(user.Id) == true))
        {
            await _repository.UpdateAsync(user);
            return NoContent();
        }
        else
        {
            return BadRequest(); 
        }
    }
}
 