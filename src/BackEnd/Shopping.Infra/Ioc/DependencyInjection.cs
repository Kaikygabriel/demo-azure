using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Application.Configurations;
using Shopping.Application.Interfaces.Queries;
using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.Interfaces.Services;
using Shopping.Infra.Configurations;
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

        #region Configurations

            builder.Services.AddTransient<StorageConfiguration>(x =>
                new StorageConfiguration(builder.Configuration["Storage:Connection"] ?? throw new Exception("Storage connection not found !"),builder.Configuration["Storage:Container"] ?? throw new Exception("storage container not found")));
            builder.Services.AddTransient<JwtConfiguration>(x =>
                new JwtConfiguration(builder.Configuration["Token:Key"] ?? throw new Exception("Token key not found !")));
        #endregion
        
        #region Queries

            builder.Services.AddTransient<IProductQuery,ProductQuery>();

        #endregion
        
        #region Repositories
            builder.Services.AddTransient<ICategoryRepository,CategoryRepository>();
            builder.Services.AddTransient<IProductRepository,ProductRepository>();
            builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
            builder.Services.AddTransient<IOrderRepository,OrderRepository>();
            builder.Services.AddTransient<IVoucherRepository,VoucherRepository>();
            builder.Services.AddTransient<IUserRepository,UserRepository>();
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