using Core.Interfaces;
using e_commerce.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service)
        {

            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            service.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                            .Where(e => e.Value.Errors.Count > 0)
                            .SelectMany(e => e.Value.Errors)
                            .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors

                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return service;
        }
    }
}
