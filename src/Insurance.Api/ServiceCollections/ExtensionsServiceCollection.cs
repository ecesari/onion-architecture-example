using CoolBlue.Products.Application.Insurance.Queries.CalculateProductInsurance;
using CoolBlue.Products.Application.ProductType.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Insurance.Api.ServiceCollections
{

    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddMediatrHandlersExplicitly(this IServiceCollection services)
        {
            var assembly = typeof(AddSurchargeCommandHandler).GetTypeInfo().Assembly;
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            //services
            //.AddTransient<IRequestHandler<AddSurchargeCommand>, AddSurchargeCommandHandler>()
            //.AddTransient<IRequestHandler<CountFruitRequest<Orange>, string>, CountFruitRequestHandler<Orange>>();

            return services;
        }
    }

}
