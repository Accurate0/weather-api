var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSingleton<WeatherApi.Services.Database>()
    .AddAutoMapper(typeof(WeatherApi.Model.Mappers.WeatherMapper))
    .AddAutoMapper(typeof(WeatherApi.Model.Mappers.WeatherDataMapper))
    .AddControllers();

if (builder.Configuration.GetValue<bool>("UseBOMWeatherService"))
{
    builder.Services.AddHostedService<WeatherApi.Services.BOMWeather>();
}

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
