using CoolBlue.Products.Application.ProductType.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Insurance.Api.ServiceCollections
{

    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddMediatrHandlersExplicitly(this IServiceCollection services)
        {
            var assembly = typeof(AddSurchargeCommandHandler).GetTypeInfo().Assembly;
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            return services;
        }
    }

}
