using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Run_Over_Test
{
    internal interface IFirst
    {
        void Something()
        {
            Console.WriteLine("Can you see the First Interface");
            using (var client = new HttpClient())
            {

            }
        }
    }
    internal interface ISecond
    {
        void Something()
        {
            Console.WriteLine("Can you see the second interface");
        }
    }
    class CheckSerial
    {
        Person person = new Person() { Name = "Olaide", Age = 29, DOB = new DateTime(1996, 05, 23) };
        string filePath = @"C:\Users\Olaide Ogunbunmi\OneDrive - Fast Credit\Desktop\Olaide File Folder\person.xml";
        XmlSerializer serializer = new XmlSerializer(typeof(Person));

        public void SerializeObjectToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, person);
            }
        }
        public void DeserializeFileToObject()
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                var deserialised = (Person)serializer.Deserialize(reader);
                Console.WriteLine($"The Name is: {deserialised.Name}, Age is: {deserialised.Age} and DOB is: {deserialised.DOB}");
            }
        }

    }

    [Serializable]
    [XmlRoot("Person Change")]
    public class Person
    {
        [XmlElement("Person_Name")]
        public string Name { get; set; }
        public int Age { get; set; }
        [XmlAttribute("Date")]
        public DateTime DOB { get; set; }
    }

    class Serialization
    {
        Animal animal = new Animal() { Name = "Cat", Age = 50 };
        private readonly string filePath = @"C:\Users\Olaide Ogunbunmi\OneDrive - Fast Credit\Desktop\Olaide File Folder\animal.json";

        public void SerializeObject()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                string jsonstringSer = JsonSerializer.Serialize(animal);
                writer.Write(jsonstringSer);
            }

        }
        public void DeserializeObject()
        {
            using(StreamReader reader = new StreamReader(filePath))
            {          
                Animal jsonDeserialise = JsonSerializer.Deserialize<Animal>(reader.ReadToEnd());
                Console.WriteLine($"{jsonDeserialise.Name} and Age is: {jsonDeserialise.Age}");
               
            }



        }
    }
    class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
