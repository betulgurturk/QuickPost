namespace QuickPostApi.Middlewares
{
    /// <summary>
    /// Global exception handler middleware
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="Next"></param>
        /// <param name="logger"></param>
        public ExceptionHandlerMiddleware(RequestDelegate Next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = Next;
            _logger = logger;
        }
        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
            }
        }
    }
}
