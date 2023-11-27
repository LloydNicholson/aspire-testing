using Aspire.BlazorUI.Components;
using Aspire.BlazorUI.Services;
using AspireTesting.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHttpClient<WeatherService>(p =>
{
    p.BaseAddress = new Uri("http://backend");
});

builder.AddAzureTableService(connectionName: "azure-tables");

builder.AddRedisOutputCache("rediscache");

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseOutputCache();

app.Run();
