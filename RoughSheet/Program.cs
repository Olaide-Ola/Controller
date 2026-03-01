using System;
using System.Threading.Tasks;

namespace RoughSheet
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Main has started....");

            Task<string> task = DoSomething();


            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                //await Task.Delay(2500);
            }
            string result = await task;

            Console.WriteLine($"Result from Do Something {result}");
            Console.WriteLine("Main has finished....");
        }
        private static async Task<string> DoSomething()
        {
            Console.WriteLine("Do Something Async has started....");
            await Task.Delay(5000);
            Console.WriteLine("Do Something Async has finished....");
            return "Done!";
        }
    }
}