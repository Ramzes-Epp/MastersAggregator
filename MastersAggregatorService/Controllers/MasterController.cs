using MastersAggregatorService.Interfaces;
using MastersAggregatorService.Models;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
public class MasterController : BaseController<Master>
{
    private readonly IMasterRepository _repository;

    public MasterController(IMasterRepository repository)
    {
        _repository = repository;
    }
 
    /// <summary>
    /// GET all masters
    /// </summary> 
    /// <returns>List of all masters in Json format</returns>
    /// <response code="200"> Returns List of all Masters in Json format.</response>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var masters = await _repository.GetAllAsync();
        if (masters.Any())
            return Ok(masters);
        else
            return NotFound();
    }
     
    /// <summary>
    /// GET master by Id
    /// </summary>
    /// <param name="Master's Id"></param>
    /// <returns>Master by id</returns>
    /// <response code="200"> Returns Master by id in Json format.</response>
    /// <response code="404"> Master does not exist.</response>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var master = await _repository.GetByIdAsync(id);

        if (master is null)
        {
            return NotFound();
        }

        return Ok(master);
    }
     
    /// <summary>
    /// GET masters by condition
    /// </summary>
    /// <param name="condition"></param>
    /// <returns>List of masters by condition in Json format</returns>
    /// <response code="200"> Returns Masters by condition in Json format.</response>
    /// <response code="404"> Masters with this condition does not exist</response>
    [HttpGet("{condition:bool}")]
    public async Task<IActionResult> GetByCondition(bool condition)
    {
        var masters = await _repository.GetByConditionAsync(condition);
        if (masters.Any())
        {
            return Ok(masters);
        }
        else
        {
            return NotFound();
        }
    }
     
    /// <summary>
    /// Create new master's
    /// </summary>
    /// <param name="master"></param>
    /// <returns></returns>
    [HttpPost] 
    public async Task<IActionResult> CreateMaster([FromBody] Master master)
    {
        //переопределен метод Equals() и сравниваем пока по id и имени (MastersName) есть ли такой Master в БД то return BadRequest()
        var masters = await _repository.GetAllAsync();

        if (masters.Any(u => u.MastersName.Equals(master.MastersName) == true))
            return BadRequest(); 

        await _repository.SaveAsync(master);
        return NoContent();
    }
     
    /// <summary>
    /// PUT to change master's 
    /// </summary>
    /// <param name="master"></param>
    /// <returns>Master with changed condition in Json format</returns>
    /// <response code="200"> Changes master's condition.</response>
    /// <response code="400"> Invalid master's model</response>
    [HttpPut] 
    public async Task<IActionResult> UpdateMaster(Master master)
    {
        var masters = await _repository.GetAllAsync();

        if (masters.Any(u => u.Id.Equals(master.Id) == true))
        {
            await _repository.UpdateAsync(master);
            return NoContent();
        }
        else
        {
            return BadRequest();
        }
    }
     
    /// <summary>
    /// Delete Master
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>    
    /// <response code="200"> Delete master </response>
    /// <response code="400"> Invalid master's model</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMaster(int id)
    {
        var master = await _repository.GetByIdAsync(id);
        if (master is null)
            return BadRequest();
        else
        {
            await _repository.DeleteAsync(master);
            return NoContent();
        }
    }
}