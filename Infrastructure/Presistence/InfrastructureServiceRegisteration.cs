using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Data;
using Presistence.Repositories;

namespace Presistence
{
    public static class InfrastructureServiceRegisteration
    {
        public static IServiceCollection AddInfrastructureRegisteration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoredDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //Configure my services [Classes and Interfaces]
            services.AddScoped<IDataBaseInitializer, DBInitializer>();

            //add unitofwork service
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;

        }
    }
}
