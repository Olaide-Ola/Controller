using MinimalApis.Extensions.Filters;
using System.ComponentModel.DataAnnotations;

namespace WebApp
{
    public class Employee_EnsureSalary : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var employee = validationContext.ObjectInstance as Employee;
            if (employee is not null && !string.IsNullOrWhiteSpace(employee.Position) && employee.Position.Equals("Manager", StringComparison.OrdinalIgnoreCase))
            {
                if (employee.Salary < 1000)
                {
                    return new ValidationResult("A manager's salary has to be greater or equal to $100");
                }
            }
            return ValidationResult.Success;
        }
    }
}
