using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Services.MappingProfiles;
using ServicesAbstraction;

namespace Services
{
    public static class ApplicationServiceRegisteration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Auto Mapper
            services.AddAutoMapper(typeof(ProductProfile).Assembly);

            //add Service Manager
            services.AddScoped<IServiceManager, ServiceManager>();

            return services;

        }
    }
}
