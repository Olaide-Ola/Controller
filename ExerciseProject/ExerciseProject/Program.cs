using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExerciseProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Olaide Ogunbunmi\source\repos\ExerciseProject\person.json";
            //File.Create(filePath);

            Person[] person = new Person[]
            {
               new Person() { Name = "Olaide",Age = 29, BirthDate = new DateOnly(1996, 05, 23), ID = 1996 },
               new Person() {Name = "Olawale", Age = 39, BirthDate = new DateOnly(1998, 07, 14), ID = 1998},
               new Person() {Name = "Toyin", Age = 65, BirthDate = new DateOnly(1962, 11, 22), ID = 1962 },

            };


            using (StreamWriter writer = new StreamWriter(filePath))
            {
                var option = new JsonSerializerOptions { WriteIndented = true };
                string personToString = JsonSerializer.Serialize(person, option);
                writer.WriteLine(personToString);
            }






            //var jsonToObject = JsonSerializer.Deserialize<Person[]>(personToString);
            //foreach (var item in jsonToObject)
            //{
            //    Console.WriteLine(item.Name);
            //}



        }
    }
    [Serializable]
    public class Person
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("birthday")]
        public DateOnly BirthDate { get; set; }

        [JsonPropertyName("id")]
        public int ID { get; set; }


    }

}
