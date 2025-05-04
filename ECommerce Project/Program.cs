
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Repositories;
using Services;
using Services.MappingProfiles;
using ServicesAbstraction;

namespace ECommerce_Project
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer(); //Swagger Related
            builder.Services.AddSwaggerGen(); //Swagger Related

            builder.Services.AddDbContext<StoredDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //Configure my services [Classes and Interfaces]
            builder.Services.AddScoped<IDataBaseInitializer,DBInitializer>();

            //add unitofwork service
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            //Auto Mapper
            builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

            //add Service Manager
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            var app = builder.Build();

            await  InitializeDbAsync(app);
           

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        //Scope  is used when i want to inject in the main program but i cant 
        //It Performed after the build 
        public static async Task InitializeDbAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbIntializer = scope.ServiceProvider.GetRequiredService<IDataBaseInitializer>();
            await dbIntializer.InitializerAsync();
        }
    }
}
