using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace ECommerce_Project.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateAPIValidationResponse(ActionContext context)
        {
           
                var errors = context.ModelState
                .Where(modelStateEntry => modelStateEntry.Value.Errors.Any())
                .Select(modelStateEntry => new ValidationError()
                {
                    Field = modelStateEntry.Key,
                    Errors = modelStateEntry.Value.Errors.Select(err => err.ErrorMessage)
                });
                var response = new ValidationErrorModel()
                {
                    validationErrors = errors 
                };
                return new BadRequestObjectResult(response);
        }
    }
}
