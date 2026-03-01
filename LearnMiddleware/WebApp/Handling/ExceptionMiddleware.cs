
namespace WebApp.Handling
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("Welcome to my Exception Custom Middleware <br>");
                await next(context);
                await context.Response.WriteAsync("Say Goodbye to my Exception Custom Middleware <br>");
            }
            catch (Exception ex)
            {
                await context.Response.WriteAsync("<h1>Error</h1>");
                await context.Response.WriteAsync($"{ex.Message}");
                
            }
            
        }
    }
}
