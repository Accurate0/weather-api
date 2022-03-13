var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSingleton<WeatherApi.Services.Database>()
    .AddAutoMapper(typeof(LibWeather.Model.Mappers.WeatherMapper))
    .AddAutoMapper(typeof(LibWeather.Model.Mappers.WeatherDataMapper))
    .AddControllers();

builder.Host.ConfigureLogging(logging => logging.AddAzureWebAppDiagnostics());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
        .UseSwaggerUI();
}

app.UseHttpsRedirection()
    .UseAuthorization();

app.MapControllers();

// setup cosmosdb
await app.Services.GetRequiredService<WeatherApi.Services.Database>().InitAsync();

app.Run();
