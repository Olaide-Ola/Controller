using System.ComponentModel.DataAnnotations;
using WebApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}
app.UseStatusCodePages();

app.MapGet("/", HtmlResult () =>
{
    string html = "<h2> Welcome to our API </h2> is used to learn ASP.NET Core";

    return new HtmlResult(html);
});

app.MapGet("/employees", IResult () =>
{
    var employees = EmployeeRepository.GetEmployees();
    return TypedResults.Ok(employees);
});

app.MapPost("/employees", (Employee employee) =>
{
    if (employee is null || employee.Id <= 0)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
       {
           {"id", new[] {"Employee is not provided or is not valid"} }
       });
    }
    EmployeeRepository.AddEmployees(employee);
    return TypedResults.Created($"/employees/{employee.Id}");
});

app.Run();


internal static class EmployeeRepository
{
    private static readonly List<Employee> employees = new List<Employee>()
        {
            new Employee(1, "John Doe", "Engineer", 60000),
            new Employee(2, "Jane Smith", "Manager", 75000),
            new Employee(3, "Sam Brown", "Technician", 50000)
        };
    public static List<Employee> GetEmployees()
    {
        return employees;
    }
    public static Employee? GetEmployee(int employ)
    {
        return employees.FirstOrDefault(x => x.Id == employ);
    }
    public static void AddEmployees(Employee? employee)
    {
        if (employee is not null)
        {
            employees.Add(employee);
        }

    }
    public static bool UpdateEmployee(Employee? employee)
    {
        if (employee is not null)
        {
            var item = employees.FirstOrDefault(emp => emp.Id == employee.Id);
            if (item is not null)
            {
                item.Name = employee.Name;
                item.Position = employee.Position;
                item.Salary = employee.Salary;
                return true;
            }
        }
        return false;
    }
    public static void DeleteEmployee(Employee? employee)
    {
        if (employee is not null)
        {
            var item = employees.FirstOrDefault(x => x.Id == employee.Id);
            if (item is not null)
            {
                employees.Remove(item);
            }
        }
    }
}

public class Employee
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Position { get; set; }
    [Required]
    [Range(5000, 200000)]
    public double Salary { get; set; }
    public Employee(int id, string name, string position, double salary)
    {
        Id = id;
        Name = name;
        Position = position;
        Salary = salary;
    }
}