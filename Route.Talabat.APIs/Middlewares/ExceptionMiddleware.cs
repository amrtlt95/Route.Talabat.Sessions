using Route.Talabat.APIs.Errors;
using System.Net;
using System.Text.Json;

namespace Route.Talabat.APIs.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IWebHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger ,IWebHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var response = env.IsDevelopment() ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    :new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                var json = JsonSerializer.Serialize(response);  
                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
