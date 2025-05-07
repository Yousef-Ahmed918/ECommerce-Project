using Domain.Contracts;
using ECommerce_Project.Factories;
using Microsoft.AspNetCore.Mvc;

enamespace ECommerce_Project
{
    public static class Extensions
    {
    public static IServiceCollection AddWebApplicationServices(this IServiceCollection services)
    {
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        AddSwaggerServices(services);

        //change Validation Configuration
        services.Configure<ApiBehaviorOptions>(options =>
        {
            //Func<ActionContext , IActionResult>
            options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateAPIValidationResponse;
        }
        );
        return services;
    }

    private static void AddSwaggerServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer(); //Swagger Related
        services.AddSwaggerGen(); //Swagger Related
    }
    public static async Task InitializeDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbIntializer = scope.ServiceProvider.GetRequiredService<IDataBaseInitializer>();
        await dbIntializer.InitializerAsync();
    }

}
