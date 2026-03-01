using WebApp.Handling;
using WebApp.MiddleComponent;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddTransient<MyCustomMiddleware>();
builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    //context.Response.Headers["MyHeader"] = "My Header Content";
    await context.Response.WriteAsync("Middleware #1: Before calling method\r\n");
    await next(context);
    await context.Response.WriteAsync("Middleware #1: After calling method \r\n");
});

//app.UseMiddleware<MyCustomMiddleware>();

//app.UseWhen((context) =>
//{
//    return context.Request.Path.StartsWithSegments("/employee") && context.Request.Query.ContainsKey("Id");
//}, appBuilder =>
//    {
//        appBuilder.Use(async (context, next) =>
//        {
//            await context.Response.WriteAsync("This is the about app\r\n");
//            await next(context);
//        });

//        appBuilder.Use(async (context, next) =>
//        {
//            await context.Response.WriteAsync("End of page\r\n");
//            await next(context);
//        });
//    });
//app.MapWhen((context) =>
//{
//    return context.Request.Path.StartsWithSegments("/employee") && context.Request.Query.ContainsKey("Id");
//}, appBuilder =>
//    {
//        appBuilder.Use(async (context, next) =>
//        {
//            await context.Response.WriteAsync("This is the about app\r\n");
//            await next(context);
//        });

//        appBuilder.Run(async context =>
//        {
//            await context.Response.WriteAsync("End of page\r\n");
//        });
//    });

//app.Map("/employee", appBuilder =>
//{
//    appBuilder.Use(async (context, next) =>
//    {
//        await context.Response.WriteAsync("This is the about app\r\n");
//        await next(context);
//    });

//    appBuilder.Run(async context =>
//    {
//        await context.Response.WriteAsync("End of page");
//    });
//});

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    throw new ApplicationException("Exception Handling");
    await context.Response.WriteAsync("Middleware #2: Before calling method\r\n");
    await next(context);
    await context.Response.WriteAsync("Middleware #2: After calling method \r\n");
});

//app.Run(async context =>
//{
//    await context.Response.WriteAsync($"This is the end of pipeline");
//});

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #3: Before calling method\r\n");
    await next(context);
    await context.Response.WriteAsync("Middleware #3: After calling method\r\n");

});

app.Run();
