using e_commerce.Errors;
using System.Net;
using System.Text.Json;

namespace e_commerce.Middleware
{
    public class ExeptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExeptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExeptionMiddleware(RequestDelegate next , ILogger<ExeptionMiddleware> logger , IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex , ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var respone = env.IsDevelopment()
                    ? new ApiExeption((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiExeption((int)HttpStatusCode.InternalServerError);

                var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json =JsonSerializer.Serialize(respone , option);
                await context.Response.WriteAsync(json);

            }
        }
    }
}
