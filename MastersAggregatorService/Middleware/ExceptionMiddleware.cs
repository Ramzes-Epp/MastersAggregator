using System.Net;
using System.Text.Json;
using MastersAggregatorService.Errors;
using MastersAggregatorService.Repositories;

namespace MastersAggregatorService.Middleware;


public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;


    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ExceptionRepository repository)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ApiException(context.Response.StatusCode, context.User.ToString(), ex.Message,
                    ex.StackTrace);

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await repository.SaveAsync(response);

            await context.Response.WriteAsync(json);
            
            throw;
        }
    }
}