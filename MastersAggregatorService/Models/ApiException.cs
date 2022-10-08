using MastersAggregatorService.Models;

namespace MastersAggregatorService.Errors;

public class ApiException : BaseModel
{
    public ApiException(int statusCode, string userName = null, string message = null, string details = null)
    {
        
        StatusCode = statusCode;
        UserName = userName;
        Message = message;
        Details = details;
    }

    public string UserName { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Details { get; set; }
    
}