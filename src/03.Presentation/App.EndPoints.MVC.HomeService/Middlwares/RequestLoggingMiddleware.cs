namespace App.EndPoints.MVC.HomeService.Middlwares
{

    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "یک خطا در سیستم رخ داد: {Message}", ex.Message);

              
                var errorMessage = Uri.EscapeDataString("متاسفانه خطایی رخ داده است");

                context.Response.Redirect($"/Home/AccessDenied?message={errorMessage}");
            }
        }
    }
}

