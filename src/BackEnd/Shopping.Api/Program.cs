using Shopping.Api.Extensions;
using Shopping.Api.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfigurations();
builder.AddDependency();
builder.AddData();

if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddExceptionHandler<ExceptionHandler>();
    builder.Services.AddProblemDetails();
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}
app.UseHsts();

app.UseHttpsRedirection();

app.UseRouting();

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();