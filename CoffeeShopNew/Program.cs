using CoffeeShopNew.Components;
using CoffeeShopNew.Context;
using CoffeeShopNew.Services;
using CoffeeShopNew.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<CoffeeShopContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("CoffeeShopDb")));

builder.Services.AddScoped<ICoffeeItemService, CoffeeItemService>();
builder.Services.AddScoped<IMiscItemService, MiscItemService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 1024 * 1024 * 5; // 5MB
});

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

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();