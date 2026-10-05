using System.Threading.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Shopping.Application.Ioc;
using Shopping.Infra.Data.Context;
using Shopping.Infra.Ioc;

namespace Shopping.Api.Extensions;

public static class BuilderExtensions
{
    public static WebApplicationBuilder AddDependency(this WebApplicationBuilder builder)
    {
        builder.AddInfra();
        builder.Services.AddApplication();
        return builder;
    }
    
    public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(x =>
            x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        return builder;
    }
    public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.WebHost.ConfigureKestrel(x =>
        {
            x.AddServerHeader = false;
        });


        builder.Services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 50,
                        QueueLimit = 0,
                        Window = TimeSpan.FromMinutes(1)
                    }));

            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        });

        
        return builder;
    }
}