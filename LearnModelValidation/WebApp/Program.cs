using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/employees", (Employee employee) =>
{
    EmployeeRepository.AddEmployees(employee);
    return "Employee is added successfuly";
}).WithParameterValidation();

app.MapGet("/registration", (string email, string password) =>
{
    return ($"Your Email Address is: {email} and Password is: {password}");
});
app.MapGet("/registration", async (context) =>
{
    var email = context.Request.Query["email"];
    var pwd = context.Request.Query["password"];
    await context.Response.WriteAsync($"Your Email Address is: {email} and Password is: {pwd}");
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
    [WebApp.Employee_EnsureSalary]
    public double Salary { get; set; }
    public Employee(int id, string name, string position, double salary)
    {
        Id = id;
        Name = name;
        Position = position;
        Salary = salary;
    }
} 