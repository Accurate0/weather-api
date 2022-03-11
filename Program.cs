var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<WeatherApi.Services.CosmosDb>();

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
await app.Services.GetRequiredService<WeatherApi.Services.CosmosDb>().InitAsync();

app.Run();
