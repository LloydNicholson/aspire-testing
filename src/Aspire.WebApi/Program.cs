using Aspire.Core;
using Aspire.WebApi;
using AspireTesting.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddAzureTableService("azure-tables");

builder.AddSqlServerDbContext<TestDbContext>(
    connectionName: "testdb",
    configureSettings: p =>
    {
        p.ConnectionString = builder.Configuration.GetConnectionString("sql");
        p.DbContextPooling = true;
        p.Metrics = true;
        p.Tracing = true;
    },
    configureDbContextOptions: p =>
    {
        p.UseSqlServer(options =>
        {
            options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        });

        p.EnableDetailedErrors();
    });

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

app.MapGet("/weatherforecast",
        (TestDbContext dbContext) =>
            dbContext.Set<WeatherForecast>().ToList())
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();
