namespace FirstApproach
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("Choose a Task");
            do
            {
                Console.WriteLine("1 - Add Student\n"+ 
                "2 - Read all Student Records\n" +
                "3 - Update Student List\n" +
                "4 - Delete Student List\n" +
                "5 - Calculator Grades\n" +
                "6 - Exit");
                Console.Write("\nEnter a task: ");
                string? userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        Student.AddStudent();
                        break;
                    case "2":
                        Student.ReadAllStudent();
                        break;
                    case "3":
                        Student.UpdateStudent();
                        break;
                    case "4":
                        Student.DeleteStudent();
                        break;
                    case "5":
                        Student.CalculateGrade();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

            } while (true);

        }
    }
    public class StudentData
    {
        
        private string _name = string.Empty;
        private int _age;
        private string _email = string.Empty;
        private Score _score;
        public string Name { get { return _name; } set { _name = value; } }
        public int Age { get { return _age; } set { _age = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public Score Score { get { return _score; } set { _score = value; } }
        public int StudentID { get; set; }
    }
    public class Score
    {
        public int English { get; set; }
        public int Mathematics { get; set; }
        public int Chemistry { get; set; }
        public int Physics { get; set; }

        public Score(int english, int mathematic, int chemistry, int physics)
        {
            English = english;
            Mathematics = mathematic;
            Chemistry = chemistry;
            Physics = physics;
        }
        public int GradeCalculator()
        {
            int result = (English + Mathematics + Physics + Chemistry) / 4;
            return result;
        }
    }
}
