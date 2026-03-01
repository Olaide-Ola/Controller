using System.Collections;

namespace TestProject
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string[] name = new [] { "Olaide", "Ogunbunmi", "Taiwo", "John" };

            MapThem mapThem = new MapThem(Show);
            mapThem += See;

            mapThem(9);
            
        }
        public delegate void MapThem(int item);
        public static void Show(int item)
        {
            Console.WriteLine($"Can you see this delegate {item}");
        }
        public static void See(int item)
        {
            Console.WriteLine($"What of the second delegate {item}");
        }
    }
}