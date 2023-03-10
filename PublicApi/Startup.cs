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
                config =>
                {
                    config.Filters.Add<NotSupportedCoinExceptionFilter>();
                    config.Filters.Add<InsufficientFundsExceptionFilter>();
                    config.Filters
                        .Add<InsufficientFundsInChangeBankExceptionFilter>();
                    config.Filters.Add<NotKnownProductExceptionFilter>();
                    config.Filters.Add<ProductOutOfStockExceptionFilter>();
                }
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

            RegisterVendingMachineServices(services);
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

        private IServiceCollection RegisterVendingMachineServices(IServiceCollection services)
        {
            var coinTill = CreateCoinTill();
            var pricingService = CreatePricingService();
            var productsRepository = CreateProductsRepository();
            var vendingDispenser = CreateVendingDispenser(
                productsRepository,
                pricingService,
                coinTill
            );

            services.AddTransient<ICoinTill>(_ => coinTill);
            services.AddTransient<IPricingService>(_ => pricingService);
            services.AddTransient<IVendingProductsRepository>(
                _ => productsRepository
            );
            services.AddSingleton<IVendingDispenser>(_ => vendingDispenser);

            return services;
        }

        private static ICoinTill CreateCoinTill()
        {
            return new CoinTill(VendingOptions.CoinTillSupportedCoins);
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

        private IVendingDispenser CreateVendingDispenser(
            IVendingProductsRepository productsRepository,
            IPricingService pricingService,
            ICoinTill coinTill
        )
        {
            return new VendingDispenser(
                productsRepository,
                pricingService,
                coinTill,
                changeBank: VendingOptions.ChangeBank
            );
        }
    }
}