
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "webroot")
});
var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async (context) =>
    {
        await context.Response.WriteAsync("Welcome to the home page");
    });
    endpoints.MapGet("/employees", async (context) =>
    {
        await context.Response.WriteAsync("Get Employees");
    });

    endpoints.MapPost("/employees", async (context) =>
    {
        await context.Response.WriteAsync("Create employees");
    });
    endpoints.MapPut("/employees", async (context) =>
    {
        await context.Response.WriteAsync("Update employees");
    });
    endpoints.MapDelete("/employees/{position}/{id}", async (context) =>
    {
        await context.Response.WriteAsync($"Delete the employees with ID {context.Request.RouteValues["id"]}");
    });

    endpoints.MapGet("/{categories=shirts}/{size?}/{id?}", async (context) =>
    {
        await context.Response.WriteAsync($"Get categories : {context.Request.RouteValues["categories"]} in size: {context.Request.RouteValues["size"]} and the ID is: {context.Request.RouteValues["id"]}");
    });

});

app.Run();

class PositionConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        throw new NotImplementedException();
    }
}
