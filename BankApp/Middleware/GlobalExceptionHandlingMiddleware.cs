using System.Net;

namespace BankApp.Middleware
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(IWebHostEnvironment webHostEnvironment, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorMessage = _webHostEnvironment.IsDevelopment() ? ex.Message : "An unexpected error has occured.";
                var errorSource = _webHostEnvironment.IsDevelopment() ? ex.Source : null;

                context.Response.Redirect($"/Home/Error?errorMessage={WebUtility.UrlEncode(errorMessage)}&errorSource={WebUtility.UrlEncode(errorSource)}");
            }
        }
    }
}
