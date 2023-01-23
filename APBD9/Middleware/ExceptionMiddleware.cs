namespace APBD9.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string Path = "logs.txt";

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                LogException(httpContext, e);
            }
        }

        private async Task LogException(HttpContext httpContext, Exception e)
        {
            using(StreamWriter sw = new StreamWriter(Path, true))
            {
                await sw.WriteLineAsync($"{DateTime.Now}, {e.ToString()}");
                await _next(httpContext);
            }
        }
    }
}
