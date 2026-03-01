using Logic.Entity;
using Logic.LogicInterface;
using ModelProject;

namespace Logic
{
    public class StudentLogic : IStudent
    {
        public List<Student> _student = new List<Student>();
        public void AddStudent()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            string name = UserInput.EnterName();
            int age = UserInput.EnterAge();
            string email = UserInput.EnterEmail();
            Grades grades = UserInput.EnterGrades();

            var newStudent = new Student
            {
                ID = _student.Count + 1,
                FullName = name,
                Age = age,
                Email = email,
                Grades = grades
            };

            _student.Add(newStudent);
            cw.WriteLine("Successfully Added");
        }
        public void ReadAllStudent()
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var student in _student)
            {
                Console.WriteLine($"\n\nID: {student.ID}" +
                    $"Full name: {student.FullName}\n" +
                    $"Age: {student.Age}\n" +
                    $"Email: {student.Email}\n" +
                    $"\n\n----------------Grades---------------\n" +
                    $"English: {student.Grades.english}, PE:{student.Grades.pe}, C# {student.Grades.csharp}, Algorithm {student.Grades.algorithm}\n" +
                    $"-------------------------------------");
            }
        }
        public void DeleteStudent()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            ReadAllStudent();

            if (_student.Count <= 0)
            {
                Console.WriteLine("\nList of student are Empty!\n");
            }
            int choice = UserInput. DeleteStudentMethod();
            var studentId = _student.Find( x => x.ID == choice );
            if (studentId == null)
            {
                _student.Remove(studentId);
                Console.WriteLine("Deleted Successful");
            }
            else Console.WriteLine("Student ID not found");
        }
        public void UpdateStudent()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            ReadAllStudent();
            int choice = UserInput.UpdateStudentMethod();
            var studentId = _student.Find(x => x.ID == choice);

            if (studentId == null)
            {
                studentId.FullName = UserInput.EnterName();
                studentId.Age = UserInput.EnterAge();
                studentId.Email = UserInput.EnterEmail();
                studentId.Grades = UserInput.EnterGrades();
                Console.WriteLine("\nSuccessfuly Updated");

            }
            else
            {
                Console.WriteLine("Student ID not found");
            }
        }
    }
}
