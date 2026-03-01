namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            People people = new People();
        }
    }
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
    }
    
}
