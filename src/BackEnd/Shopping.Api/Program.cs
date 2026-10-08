using Shopping.Api.Extensions;
using Shopping.Api.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfigurations();
builder.AddDependency();
builder.AddData();
builder.AddDocumentation();

if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddExceptionHandler<ExceptionHandler>();
    builder.Services.AddProblemDetails();
}

var app = builder.Build();

app.MapOpenApi("/openapi/{documentName}.json");
app.UseSwaggerUI(x => x.SwaggerEndpoint(builder.Configuration["Url"]+"/openapi/v1.json" ?? throw new Exception("Url not found!"), ""));

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