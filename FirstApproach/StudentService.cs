using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstApproach
{
    internal static class StudentService
    {
        public static string EnterName()
        {
            do
            {
                Console.Write("Enter full name: ");
                string? name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name cannot be empty. Please enter a valid name.");
                    continue;
                }
                return name; 
            } while (true);
        }

        public static int EnterAge()
        {
            do
            {
                Console.Write("Enter Age: ");
                string? userInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userInput) || !int.TryParse(userInput, out int age))
                {
                    Console.WriteLine("Please enter a valid age.");
                    continue;
                }
                return age; 
            } while (true);

        }

        public static string EnterEmail()
        {
            do
            {
                Console.Write("Enter email address: ");
                string? email = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(email) || !(email.Contains('@') && email.Contains(".com")))
                {
                    Console.WriteLine("Invalid email adress. Enter a valid email.");
                    continue;
                }
                return email; 
            } while (true);
        }

        public static Score EnterScore()
        {
            do
            {
                Console.Write("English score: ");
                string? englishScore = Console.ReadLine();

                Console.Write("Mathematics: ");
                string? mathsScore = Console.ReadLine();

                Console.Write("Chemistry: ");
                string? chemScore = Console.ReadLine();

                Console.Write("Physics: ");
                string? phyScore = Console.ReadLine();

                if (int.TryParse(englishScore, out int english) && int.TryParse(mathsScore, out int maths) && int.TryParse(chemScore, out int chem) && int.TryParse(phyScore, out int phy))
                {
                    if ((english >= 0 && english <= 100) && (maths >= 0 && maths <= 100) && (chem >= 0 && chem <= 100) && (phy >= 0 && phy <= 100))
                    {
                        return new Score(english, maths, chem, phy);

                    }
                    else Console.WriteLine("Score cannot be less than 0 or greater than 100.");
                }
                else
                {
                    Console.WriteLine("Enter a valid score");
                } 
            } while (true);

        }
        public static int ComputeGrade()
        {
            Console.Write("Enter student ID number: ");
            string? userInput = Console.ReadLine();
            if(!int.TryParse(userInput, out int userID))
            {
                Console.WriteLine("Enter the right Student ID number.");
            }
            return userID;
        }
        public static int GetID()
        {
            do
            {
                Console.Write("\nEnter student ID number: ");
                string? userInput = Console.ReadLine();
                if (!int.TryParse(userInput, out int ID))
                {
                    Console.WriteLine("Invalid student ID.");
                    continue;
                }
                return ID; 
            } while (true);
        }
    }
}
