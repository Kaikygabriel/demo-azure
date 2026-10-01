using Microsoft.EntityFrameworkCore;
using Shopping.Application.Ioc;
using Shopping.Infra.Data.Context;
using Shopping.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.WebHost.ConfigureKestrel(x =>
{
    x.AddServerHeader = false;
});

builder.Services.AddDbContext<AppDbContext>(x =>
    x.UseNpgsql("Server=127.0.0.1;Port=5432;Database=louja;User Id=postgres;Password=Kaiky@2048;"));

builder.AddInfra();
builder.Services.AddApplication();

var app = builder.Build();

app.UseHsts();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
