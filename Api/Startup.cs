using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using VirtualVendingMachine.Filters;
using VirtualVendingMachine.Tills;
using VirtualVendingMachine.Vending;
using VirtualVendingMachine.Vending.Models;

namespace VirtualVendingMachine
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(
                config => config.Filters.Add<NotSupportedCoinExceptionFilter>()
            );

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(
                        "v1",
                        new OpenApiInfo
                        {
                            Title = "Virtual Vending Machine API",
                            Version = "v1"
                        }
                    );
                }
            );

            services.AddMediatR(typeof(Startup));

            services.AddScoped<ICoinTill, CoinTill>();
            services.AddScoped<IPricingService>(_ => CreatePricingService());
            services.AddScoped<IVendingProductsRepository>(
                _ => CreateProductsRepository()
            );
            services.AddScoped<IVendingDispenser, VendingDispenser>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    c => c.SwaggerEndpoint(
                        "/swagger/v1/swagger.json",
                        "Virtual Vending Machine API v1"
                    )
                );
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static PricingService CreatePricingService()
        {
            return new PricingService(VendingOptions.ProductPricing);
        }

        private static IVendingProductsRepository CreateProductsRepository()
        {
            var productsRepository = new VendingProductsRepository();
            var initialStock = VendingOptions.InitialStock.SelectMany(
                kvp => Enumerable.Repeat(
                    element: StockItem.Create(kvp.Key),
                    count: kvp.Value
                )
            );
            productsRepository.AddToStock(initialStock);

            return productsRepository;
        }
    }
}