var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<WeatherApi.Services.Database>();
builder.Services.AddHostedService<WeatherApi.Services.BOMWeather>();

builder.Services.AddAutoMapper(typeof(WeatherApi.Model.Mapper.WeatherMapper));
builder.Services.AddAutoMapper(typeof(WeatherApi.Model.Mapper.WeatherDataMapper));
builder.Services.AddAutoMapper(typeof(WeatherApi.Model.Mapper.CurrentWeatherMapper));

builder.Host.ConfigureLogging(logging => logging.AddAzureWebAppDiagnostics());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// setup cosmosdb
await app.Services.GetRequiredService<WeatherApi.Services.Database>().InitAsync();

app.Run();
