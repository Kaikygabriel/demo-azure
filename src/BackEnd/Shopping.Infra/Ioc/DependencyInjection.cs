using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Application.Interfaces.Queries;
using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.Interfaces.Services;
using Shopping.Infra.Query;
using Shopping.Infra.Repositories;
using Shopping.Infra.Services;
using Stripe;
using TokenService = Shopping.Infra.Services.TokenService;

namespace Shopping.Infra.Ioc;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddInfra(this WebApplicationBuilder builder)
    {
        #region Queries

            builder.Services.AddTransient<IProductQuery,ProductQuery>();

        #endregion
        
        #region Repositories
            builder.Services.AddTransient<ICategoryRepository,CategoryRepository>();
            builder.Services.AddTransient<IProductRepository,ProductRepository>();
            builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
        #endregion

        #region Services

            builder.Services.AddTransient<IStorageService, StorageService>();
            builder.Services.AddTransient<ITokenService,TokenService>();
            builder.Services.AddTransient<IPayService,PayService>();
            
        #endregion

        #region  stripe

            StripeConfiguration.ApiKey =
                builder.Configuration["Stripe:ApiKey"] ?? throw new Exception("Stripe api key not found !");
        #endregion
        return builder;
    }
}