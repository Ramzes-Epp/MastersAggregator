namespace MastersAggregatorService.Models;

public class Order : BaseModel
{ 
    public User Sender { get; init; }   

    public IEnumerable<Image> Images { get; init; }
}