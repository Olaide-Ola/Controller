using Assignment;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.UseRouting();

app.UseEndpoints(endpoint =>
{
    //endpoint.MapGet("/employees", async (HttpContext context) =>
    //{
    //    context.Response.ContentType = "text/html";
    //    var emp = EmployeeRepository.GeEmployee();
    //    foreach (var item in emp)
    //    {
    //        await context.Response.WriteAsync($"ID: {item.Id}, Name: {item.Name}, Position: {item.Position}, Salary: {item.Salary}<br>");
            
    //    }
    //});
    //endpoint.MapPost("/employees", async (HttpContext context) =>
    //{
    //    using var reader = new StreamReader(context.Request.Body);
    //    var body = await reader.ReadToEndAsync();
    //    var emp = JsonSerializer.Deserialize<Employee>(body);
    //    EmployeeRepository.AddEmployee(emp);
    //    await context.Response.WriteAsync("Employee Added Successfully");
    //});
    endpoint.MapGet("/employees", (int[] id) =>
    {
        var employees = EmployeeRepository.GeEmployee();
        var emps = employees.Where(x => id.Contains(x.Id)).ToList();
        return emps;
    });
    endpoint.MapPost("/employees", ([FromBody]Employee employee) =>
    {
        if (employee is null || employee.Id <= 0)
        {
            return "Employee is not provided or not valid";
        }
        EmployeeRepository.AddEmployee(employee);
        return "Employee Added Successfully";
    });
    endpoint.MapGet("/people", (Person? p) =>
    {
        return $"Id is {p?.Id}, Name is {p?.Name}";
    });
});
 
app.Run();

public class GetEmployeeParameter
{
    [FromRoute]
    public int id { get; set; }
    [FromQuery]
    public string name { get; set; }
    [FromHeader]
    public string position { get; set; }
}
class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public static ValueTask<Person?> BindAsync(HttpContext context)
    {
        var idStr = context.Request.Query["id"];
        var nameStr = context.Request.Headers["name"];
        if (int.TryParse(idStr, out var id))
        {
            return new ValueTask<Person?>(new Person { Id = id, Name = nameStr });

        }
        return new ValueTask<Person?>(Task.FromResult<Person?>(null));
    }
}