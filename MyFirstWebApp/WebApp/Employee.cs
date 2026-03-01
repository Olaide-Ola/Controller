namespace WebApp
{
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
            else Console.WriteLine("No recond found!");
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
    internal class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
        public Employee(int id, string name, string position, double salary)
        {
            Id = id;
            Name = name;
            Position = position;
            Salary = salary;
        }
    }
}