namespace MastersAggregatorService.Models;

public abstract class BaseModel
{
    /// <summary>
    /// Id from the database
    /// When creating an object in the code, specify the identifier as 0
    /// </summary>
    public int Id { get; init; }
}