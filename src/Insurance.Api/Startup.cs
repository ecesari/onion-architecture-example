using CoolBlue.Products.Application.Common.Interfaces;
using CoolBlue.Products.Application.Common.Services;
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
            services.AddTransient<IInsuranceService, InsuranceService>();
            services.AddTransient<IProductDataIntegrationService, ProductDataIntegrationService>();
            services.AddTransient<IHttpService, HttpService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
