using System.Net;
using System.Text.Json;
using Domain.Exceptions;
using Shared.ErrorModels;

namespace ECommerce_Project.Middlewares
{
    public class CustomeExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomeExceptionHandlerMiddleware> _logger;

        public CustomeExceptionHandlerMiddleware(RequestDelegate next,ILogger<CustomeExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                //if endpoint is not found 
                await HandleNotFoundEndPoint(context);
            }
            catch (Exception ex)
            {
                await HandleCatchException(context, ex);
            }
        }

        private static async Task HandleCatchException(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, $"Something Went Wrong");
            //Response Object With Content Type , Status Code 

            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                //StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
            response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };
            context.Response.StatusCode = response.StatusCode;

            //Return As Json
            var jsonResult = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResult);
        }

        private static async Task HandleNotFoundEndPoint(HttpContext context)
        {
            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                context.Response.ContentType = "application/json";
                var response = new ErrorDetails()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ErrorMessage = $"End Point With This Path:{context.Request.Path} Is Not Found!"
                };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
