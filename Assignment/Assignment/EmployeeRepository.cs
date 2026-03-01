namespace Assignment
{
    public class Employee
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
    public class EmployeeRepository
    {
        private static List<Employee> employees = new List<Employee>()
        {
            new Employee(1, "John Doe", "Engineer", 60000),
            new Employee(2, "Jane Smith", "Manager", 75000),
            new Employee(3, "Sam Brown", "Engineer", 50000)
        };
        public static List<Employee> GeEmployee()
        {
            return employees;
        }
        public static void AddEmployee(Employee employee)
        {
            if (employee is not null) employees.Add(employee);

        }
        public static Employee? GetEmployeeId(int empID)
        {
            return employees.FirstOrDefault(x => x.Id == empID);
        }
    }

}
