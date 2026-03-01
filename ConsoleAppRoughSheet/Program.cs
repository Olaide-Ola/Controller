namespace ConsoleAppRoughSheet
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Main started");

            // Start the async task but don't await it yet
            Task<string> task = DoSomethingAsync();

            // Do other work while DoSomethingAsync is running
            Console.WriteLine("Main is doing other work...");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Main working... {i}");
                //await Task.Delay(500); // Simulate work
            }

            // Now await the result
            string result = await task;
            Console.WriteLine($"Result from DoSomethingAsync: {result}");

            Console.WriteLine("Main finished");
        }

        static async Task<string> DoSomethingAsync()
        {
            Console.WriteLine("DoSomethingAsync started");
            await Task.Delay(2000); // Simulate long-running work
            //Thread.Sleep(5000);
            Console.WriteLine("DoSomethingAsync finished");
            return "Done!";
        }
    }
}