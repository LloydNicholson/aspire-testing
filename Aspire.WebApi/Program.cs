using Aspire.Core;
using Aspire.WebApi;
using AspireTesting.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddSqlServerDbContext<TestDbContext>("testdb");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast",
        () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast(
                        Date: DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC: Random.Shared.Next(-20, 55),
                        Summary: summaries[Random.Shared.Next(summaries.Length)]))
                .ToArray();
            return forecast;
        })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();
