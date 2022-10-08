using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;


[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
public class ImageController : BaseController<Image> 
{ 
    private IImageRepository _repository { get; set; }
    public ImageController(IImageRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// GET image by id
    /// </summary>
    /// <param name="Image Id"></param>
    /// <returns>Image by id</returns>
    /// <response code="200"> Returns Image by id in Json format.</response>
    /// <response code="404"> Image does not exist.</response>
    [HttpGet]
    public async Task<IActionResult> GetImageById(int id)
    {
        var image = await _repository.GetByIdAsync(id);
        if (image is null)
            return NotFound();
        else
             return Ok(new JsonResult(image));
    }
    
    /// <summary>
    /// GET all images
    /// </summary> 
    /// <returns>List of all images in Json format</returns>
    /// <response code="200"> Returns List of all images in Json format.</response>
    /// <response code="404"> Images not found.</response>
    [HttpGet("{id:int}")] 
    public async Task<IActionResult> GetAllImages()
    {
        var images = await _repository.GetAllAsync();
        if (images.Any())
            return Ok(images);
        else
            return NotFound();
    }
    /// <summary>
    /// Delete image by id
    /// </summary> 
    /// <returns></returns>
    /// <response code="204"> Image deleted</response>
    /// <response code="404"> Image not found</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteImage(int id)
    {
        var image = await _repository.GetByIdAsync(id);
        if (image is null)
            return NotFound();
        else
        {
            await _repository.DeleteAsync(image);
            return NoContent();
        }
    }
    /// <summary>
    /// POST to create image
    /// </summary> 
    /// <returns></returns>
    /// <response code="204"> Image created</response>
    /// <response code="404"> Invalid model</response>
    [HttpPost]
    public async Task<IActionResult> CreateImage([FromBody] Image image)
    {
        var images = await _repository.GetAllAsync(); 
        
        if (images.Any(x => x.Equals(image)))
            return BadRequest();

        await _repository.SaveAsync(image);
        return NoContent();
    }
    
    /// <summary>
    /// PUT to update image
    /// </summary> 
    /// <returns></returns>
    /// <response code="204"> Image updated</response>
    /// <response code="400"> Invalid model</response>
    [HttpPut]
    public async Task<IActionResult> UpdateImage([FromBody] Image image)
    {
        var images = await _repository.GetAllAsync();

        if (images.Any(x => x.Equals(image)))
        {
            await _repository.UpdateAsync(image);
            return NoContent();
        }
        return BadRequest();
    }
}