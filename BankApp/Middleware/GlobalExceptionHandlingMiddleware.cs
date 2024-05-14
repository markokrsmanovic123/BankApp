using Serilog;
using System.Net;

namespace BankApp.Middleware
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GlobalExceptionHandlingMiddleware(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                context.Response.Redirect($"/Home/Error?errorMessage={ex.Message}&errorSource={ex.Source}");
            }
        }
    }
}
