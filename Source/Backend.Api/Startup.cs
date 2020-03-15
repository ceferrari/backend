using System.Collections.Generic;
using System.Globalization;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.SQS;
using Backend.Application.Services;
using Backend.Domain.Core.Bus;
using Backend.Domain.Core.Commands;
using Backend.Domain.Core.Notifications;
using Backend.Domain.Core.Queries;
using Backend.Domain.Models.CartModel;
using Backend.Domain.Models.CartModel.Commands;
using Backend.Domain.Models.CartModel.Queries;
using Backend.Domain.Models.CartModel.Repositories;
using Backend.Domain.Models.ProductModel;
using Backend.Domain.Models.ProductModel.Commands;
using Backend.Domain.Models.ProductModel.Queries;
using Backend.Domain.Models.ProductModel.Repositories;
using Backend.Domain.Services;
using Backend.Infra.Repositories;
using Backend.Shared.Constants;
using Backend.Shared.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton(serviceProvider => new AmazonDynamoDBClient(Program.CredentialsCarlos, RegionEndpoint.USEast1));
            services.AddSingleton(serviceProvider => new AmazonSQSClient(Program.CredentialsLeandro, RegionEndpoint.USEast1));

            services.AddScoped<IBus, Bus>();
            services.AddScoped<INotificationHandler, NotificationHandler>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            // Application
            // -------------------------------------------
            // Services
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartService, CartService>();
            // -------------------------------------------

            // Domain
            // -------------------------------------------
            // Services
            services.AddScoped<IQueueService, QueueService>();

            // Cart Commands
            services.AddTransient<ICommandHandler<CartCheckoutCommand, bool>, CartCheckoutCommandHandler>();
            services.AddTransient<ICommandHandler<CartCreateCommand, Cart>, CartCreateCommandHandler>();
            services.AddTransient<ICommandHandler<CartDeleteCommand, bool>, CartDeleteCommandHandler>();
            services.AddTransient<ICommandHandler<CartDeleteItemCommand, bool>, CartDeleteItemCommandHandler>();
            services.AddTransient<ICommandHandler<CartUpdateCommand, Cart>, CartUpdateCommandHandler>();

            // Cart Queries
            services.AddTransient<IQueryHandler<CartGetAllQuery, List<Cart>>, CartGetAllQueryHandler>();
            services.AddTransient<IQueryHandler<CartGetByCustomerIdQuery, Cart>, CartGetByCustomerIdQueryHandler>();
            services.AddTransient<IQueryHandler<CartGetByIdQuery, Cart>, CartGetByIdQueryHandler>();

            // Product Commands
            services.AddTransient<ICommandHandler<ProductCreateCommand, Product>, ProductCreateCommandHandler>();

            // Product Queries
            services.AddTransient<IQueryHandler<ProductGetAllQuery, List<Product>>, ProductGetAllQueryHandler>();
            services.AddTransient<IQueryHandler<ProductGetByIdQuery, Product>, ProductGetByIdQueryHandler>();
            services.AddTransient<IQueryHandler<ProductGetBySkuQuery, Product>, ProductGetBySkuQueryHandler>();
            // -------------------------------------------
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CultureInfo.DefaultThreadCurrentCulture = Configurations.CultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = Configurations.CultureInfo;
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(Configurations.CultureInfo),
                SupportedCultures = new List<CultureInfo>
                {
                    Configurations.CultureInfo
                },
                SupportedUICultures = new List<CultureInfo>
                {
                    Configurations.CultureInfo
                }
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
