using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelProject;

namespace Logic.Entity
{
    public class UserInput
    {
        public static char Tasking()
        {
            StudentLogic _student = new StudentLogic();
            while (true)
            {
                Console.WriteLine("\n\nChoose a task: \n");

                Console.WriteLine("A - Add student\n" +
                    "R - Read all student records\n" +
                    "U - Update student record\n" +
                    "D - Delete student List\n" +
                    "G - Calculate grade\n" +
                    "E - exit\n");

                Console.Write("Anser: ");
                string userInput = Console.ReadLine();

                if (char.TryParse(userInput, out char task))
                {
                    if (task == 'E')
                    {
                        return 'E';
                    }
                        
                    switch (task)
                    {
                        case 'A':
                            _student.AddStudent();
                            break;
                        case 'R':
                            _student.ReadAllStudent();
                            break;
                        case 'U':
                            _student.UpdateStudent();
                            break;
                        case 'D':
                            _student.DeleteStudent();
                            break;
                        case 'G':
                            EnterGrades();
                            break;
                        default:
                            Console.WriteLine("Invalid input!");
                            break;
                    }
                }
            }
        }
        public static string EnterName()
        {
            while (true)
            {
                Console.WriteLine("Enter Full name: ");
                string userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine($"{nameof(userInput)} cannot be empty");
                    continue;
                }
                return userInput;
            }
        }
        public static int EnterAge()
        {
            while (true)
            {
                Console.WriteLine("Enter Age: ");
                string userAge = Console.ReadLine();

                if (!int.TryParse(userAge, out int age))
                {
                    Console.WriteLine($"{nameof(userAge)} is invalid");
                    continue;
                }
                if (age <= 5)
                {
                    Console.WriteLine("Age cannot be less than 5");
                    continue;
                }
                else if (age > 100)
                {
                    Console.WriteLine("Age cannot be greater than 100");
                    continue;
                }
                return age;
            }
        }
        public static string EnterEmail()
        {
            while (true)
            {
                Console.WriteLine("Enter Email: ");
                string userEmail = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userEmail))
                {
                    Console.WriteLine("Email cannot be empty");
                    continue;
                }
                if (!userEmail.Contains('@') && !userEmail.Contains("com"))
                {
                    Console.WriteLine("Invalid email address");
                    continue;
                }
                return userEmail;
            }
        }
        public static Grades EnterGrades()
        {
            while (true)
            {
                Console.WriteLine("Insert your name\n");

                Console.Write("English: ");
                string english = Console.ReadLine();

                Console.Write("P.E: ");
                string pe = Console.ReadLine();

                Console.Write("C#: ");
                string csharp = Console.ReadLine();

                Console.Write("Algorithm: ");
                string algorithm = Console.ReadLine();

                if (!decimal.TryParse(english, out decimal English) || !decimal.TryParse(pe, out decimal PE) || !decimal.TryParse(csharp, out decimal Sharp) || !decimal.TryParse(algorithm, out decimal Algorithm))
                {
                    Console.WriteLine("Invalid input");
                }
                else if (English <= 73 || PE <= 73 || Sharp <= 73 || Algorithm <= 73 || English > 100 || PE > 100 || Sharp > 100 || Algorithm > 100)
                {
                    Console.WriteLine("Grades cannnot be less than 73 or cannot be greater than 100");
                }

                
                var finalGrades = FinalGrades(English, PE, Sharp, Algorithm);
                Console.WriteLine($"Your final grade is {finalGrades}");

                return new Grades
                {
                    english = English,
                    pe = PE,
                    csharp = Sharp,
                    algorithm = Algorithm
                };
            }
        }
        public static decimal FinalGrades(decimal a, decimal b, decimal c, decimal d)
        {
           
            decimal finalGrade = (a + b + c + d) / 4;
            return finalGrade;
        }
        public static int UpdateStudentMethod()
        {
            Console.WriteLine("Enter student ID you want to update: ");
            string input = Console.ReadLine();

            while (true)
            {
                if(!int.TryParse(input, out int userInput))
                {
                    Console.WriteLine("Invalid input try again");
                    continue;
                }
                return userInput;
            }
        }
        public static int DeleteStudentMethod()
        {
            Console.Write("Enter student ID you want to delete: ");
            string input = Console.ReadLine();
            while (true)
            {
                if (!int.TryParse(input, out int userInput))
                {
                    Console.WriteLine("Invalid input, please try again");
                    continue;
                }
                return userInput;
            }
        }
    }
}
