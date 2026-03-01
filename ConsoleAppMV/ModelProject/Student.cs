using System.Diagnostics;

namespace ModelProject
{
    public class Student
    {
        public int ID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public Grades Grades { get; set; }
    }
    public class Grades
    {
        public decimal English { get; set; }
        public decimal P_E { get; set; }
        public decimal CSharp { get; set; }
        public decimal Algorithm { get; set; }

    }
}
