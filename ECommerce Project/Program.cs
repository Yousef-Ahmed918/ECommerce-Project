
using Azure;
using Domain.Contracts;
using ECommerce_Project.Factories;
using ECommerce_Project.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Data;
using Presistence.Repositories;
using Services;
using Services.MappingProfiles;
using ServicesAbstraction;
using Shared.ErrorModels;

namespace ECommerce_Project
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddWebApplicationServices();

            //Refactor the services
            builder.Services.AddInfrastructureRegisteration(builder.Configuration);
            builder.Services.AddServices(); 
            
            var app = builder.Build();

            await  app.InitializeDbAsync();


            //Custom Exception Middleware
            app.UseMiddleware<CustomeExceptionHandlerMiddleware>();

            

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
        
    }
}
