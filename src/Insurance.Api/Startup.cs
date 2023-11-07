using CoolBlue.Products.Application.Common.Interfaces;
using CoolBlue.Products.Application.Common.Services;
using CoolBlue.Products.Application.ProductType.Commands;
using CoolBlue.Products.Domain.Repositories;
using CoolBlue.Products.Infrastructure.Data;
using CoolBlue.Products.Infrastructure.HttpOperations;
using CoolBlue.Products.Infrastructure.Integration;
using CoolBlue.Products.Infrastructure.Repository;
using CoolBlue.Products.Infrastructure.Repository.Base;
using CoolBlue.Products.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using MediatR;
using Insurance.Api.ServiceCollections;

namespace Insurance.Api
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
            services.AddControllers();

            //add product api settings
            var productApiSettingsSection = Configuration.GetSection("ProductApiSettings");
            services.Configure<ProductApiSettings>(productApiSettingsSection);

            //db
            services.AddDbContext<InsuranceDbContext>(options => options.UseInMemoryDatabase(databaseName: "InsuranceDb"));

            //swagger
            services.AddSwaggerGen();

            //Dependencies
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IInsuranceService, InsuranceService>();
            services.AddScoped<IProductDataIntegrationService, ProductDataIntegrationService>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddHttpClient<HttpService>();


            services.AddMediatrHandlersExplicitly();
            #region MediatR
            //services.AddMediatR(typeof(Startup));
            //services.AddMediatR(typeof(AddSurchargeCommand).GetTypeInfo().Assembly);
            //services.AddMediatR(typeof(UpdateProductCommand).GetTypeInfo().Assembly);
            //services.AddMediatR(typeof(DeleteProductCommand).GetTypeInfo().Assembly);
            //services.AddMediatR(typeof(SearchProductsQuery).GetTypeInfo().Assembly);
            //services.AddMediatR(typeof(GetProductsByStockRangeQuery).GetTypeInfo().Assembly);
            //services.AddMediatR(typeof(GetProductsQuery).GetTypeInfo().Assembly);
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
