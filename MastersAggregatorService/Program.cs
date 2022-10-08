using MastersAggregatorService.Interfaces;
using MastersAggregatorService.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.OpenApi.Models;
using MastersAggregatorService.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(config => {
    config.SwaggerDoc("v1", new OpenApiInfo() { Title = "WebAPI", Version = "v1" });
    config.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Name = "ApiKey",//новый ключ который передаеться в heder (запросе с сайта)
        Description = "Введите Api ключ для от вашего кабинета ( ТЕСТОВЫЙ ключ Keyfwsefso987kcxfv )",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "basic"
    });
    config.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Basic"
                        }
                    },
                    Array.Empty<string>()
                }
            });
    builder.Services.AddCors(); // возможно следует удалить в будущем

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
});

//Добавили в сервис наши Repository 
builder.Services.AddScoped<IImageRepository, ImageRepository>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IOrderRepository, OrderRepository>()
    .AddScoped<IMasterRepository, MasterRepository>()
    .AddScoped<TokenRepository>()
    .AddScoped<IExceptionRepository, ExceptionRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseMiddleware<ExceptionMiddleware>();   
}
//добавляем сервис авторизации в Middleware
app.UseMiddleware<ApiKeyAuthentication>();

app.UseCors(policy => policy.AllowAnyMethod().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
