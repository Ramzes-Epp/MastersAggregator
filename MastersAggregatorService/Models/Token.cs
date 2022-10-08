namespace MastersAggregatorService.Models;

public class Token : BaseModel
{
    public string ApiToken { get; set; }
    public string ApiUserName { get; set; }
}