
using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;


namespace MastersAggregatorService.Middleware
{ 
    public class ApiKeyAuthentication  
    {  
        private readonly RequestDelegate _next;
    
        public ApiKeyAuthentication(RequestDelegate next)
        { 
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, TokenRepository reposToken)
        { 
            //получеам с Header элемент ApiKey, если его нет то ошибка
            if (!context.Request.Headers.TryGetValue("ApiKey", out var extractedApiKey))// ApiKey - в Swagger назначили как ключ в заголовке куда запишеться Api ключ в Request.Headers
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Api Key was not provided. (Using ApiKeyAuthentication) ");
                return;
            } 

            //получаем Api Token с запроса от клиента
            string strApiToken = extractedApiKey.First();
            //Получаем коллекцию Token из БД
            IEnumerable<Token> allApiToken = await reposToken.GetAllApiKeyBdAsync();
 
            //проверяем есть ли такой токен в БД
            Token? chekTokenValue = allApiToken.FirstOrDefault(token => token.ApiToken == strApiToken, null);

            //проверяем сушестует токен - если он null то ошибка авторизации 403
            if (chekTokenValue == null)
            {
                context.Response.StatusCode = 403; 
                await context.Response.WriteAsync("Unauthorized client. (Using ApiKeyAuthentication)");
                return;
            }

            //Если Api токен не null создаем и записываем в него UserName связанное с этим Api в БД master_shema.access
            context.Items.Add("ApiUserName", chekTokenValue.ApiUserName);
             

            await _next(context);
        }  
    } 

}
 
