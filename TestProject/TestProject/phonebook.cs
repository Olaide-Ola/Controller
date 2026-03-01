using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    internal class phonebook
    {
        public static void Sample()
        {
            Phonebook phonebook = new Phonebook();

            Console.WriteLine("Please pick an option below\n" +
                    "1. Add Contact\n" +
                    "2. View all contacts\n" +
                    "3. Search for contact by Phone number\n" +
                    "4. Search for contacts by Matching names\n" +
                    "5. Search for contacts by Name\n" +
                    "6. Delete a contact\n" +
                    "7. Rename or update a contact\n" +
                    "7. Exit");

            while (true)
            {
                Console.WriteLine("what is your option: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        phonebook.AddContacts();
                        break;
                    case "2":
                        phonebook.ViewAllContact();
                        break;
                    case "3":
                        phonebook.SearchContactByNumber();
                        break;
                    case "4":
                        phonebook.SearchContactsByMatchingName();
                        break;
                    case "5":
                        phonebook.SearchContactsByName();
                        break;
                    case "6":
                        Contact contact = new Contact();
                        Console.WriteLine("What is the first name of the contact you want to delete: ");
                        string contactFirstName = Console.ReadLine();
                        Console.WriteLine("What is the seconf name of the contact you want to delete: ");
                        string contactSecondName = Console.ReadLine();
                        Console.WriteLine("What is the phone number: ");
                        string contactPhonenumber = Console.ReadLine();

                        contact.LastName = contactSecondName;
                        contact.FirstName = contactFirstName;
                        contact.Number = contactPhonenumber;
                        phonebook.DeleteContact(contact);
                        break;
                    case "7":
                        var result = phonebook.UpdateContact();

                        if (result != null)
                        {
                            Console.WriteLine("Enter new first name: ");
                            string newFirstName = Console.ReadLine();
                            result.FirstName = newFirstName;


                            Console.WriteLine("Enter new second name: ");
                            string newSecondName = Console.ReadLine();
                            result.LastName = newSecondName;

                            Console.WriteLine("Enter new phone number: ");
                            string newPhonenumber = Console.ReadLine();
                            result.Number = newPhonenumber;

                            Console.WriteLine("Conttact updated successfully.");
                        }
                        else Console.WriteLine("Contact not found");
                        break;
                    default:
                        break;


                }
            }

        }
        
        }
    }
    public class Contact
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _phoneNumber = string.Empty;
    public string FirstName { get { return _firstName; } set => _firstName = value; }
    public string LastName { get { return _lastName; } set => _lastName = value; }
    public string Number { get { return _phoneNumber; } set => _phoneNumber = value; }


}
public class Phonebook
{
    private List<Contact> _contacts = new List<Contact>();
    public void AddContacts()
    {
        Contact contact = new Contact();

        Console.WriteLine("What is your first name: ");
        string userFirstName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(userFirstName))
        {
            Console.WriteLine($"{nameof(userFirstName)} cannot be blank.");
            return;

        }
        else
        {
            contact.FirstName = userFirstName;
        }


        Console.WriteLine("What is your last name: ");
        string userLastName = Console.ReadLine();
        contact.LastName = userLastName;

        Console.WriteLine("What is your number: ");
        string userPhonenumber = Console.ReadLine();
        contact.Number = userPhonenumber;


        _contacts.Add(contact);
    }
    public void ViewAllContact()
    {
        for (int i = 0; i < _contacts.Count; i++)
        {
            var list = _contacts[i];
            Console.WriteLine($"{i + 1}: {list.FirstName} {list.LastName}, {list.Number}");

        }
    }
    public void SearchContactByNumber()
    {
        Console.WriteLine("What is the phone number you want to search with: ");
        string userInput = Console.ReadLine();
        Contact contactName = _contacts.FirstOrDefault(x => x.Number == userInput);
        if (contactName != null)
        {
            Console.WriteLine($"The contact name is: {contactName.FirstName} {contactName.LastName}");
        }
        else if (contactName == null)
        {
            Console.WriteLine("Wrong phone number");
            return;
        }

    }
    public void SearchContactsByMatchingName()
    {
        string userInputName = Console.ReadLine();
        var matchedContacts = _contacts.Where(x => $"{x.FirstName} {x.LastName}".Contains(userInputName, StringComparison.Ordinal)).ToList();
        if (matchedContacts.Count > 0)
        {
            foreach (var item in matchedContacts)
            {
                Console.WriteLine($"The phone numbers are {item.Number}");
            }
        }
        else
        {
            Console.WriteLine("No contact found");
        }
    }
    public void SearchContactsByName()
    {
        string userInputName = Console.ReadLine();
        var result = _contacts.FirstOrDefault(x => $"{x.FirstName} {x.LastName}" == userInputName);
        if (result != null)
            Console.WriteLine($"The phone number is: {result.Number}");
        else Console.WriteLine("No contact found!");
    }
    public void DeleteContact(Contact removeContact)
    {
        var deleteContact = _contacts.FirstOrDefault(x => x.FirstName == removeContact.FirstName && x.LastName == removeContact.LastName);
        if (deleteContact != null)
        {
            _contacts.Remove(deleteContact);
        }
        else
        {
            Console.WriteLine("Please try again, incorrect details");
        }

    }
    public Contact UpdateContact() //Olaide Improve this code. Search for how I can collect them in a list
    {
        Contact renameContact = new Contact();


        Console.WriteLine("Please enter the first name: ");
        string userInputFirstName = Console.ReadLine();

        Console.WriteLine("Please the last name: ");
        string userInputLastName = Console.ReadLine();

        Console.WriteLine("Please enter the phone number: ");
        string userPhoneNumber = Console.ReadLine();

        renameContact.FirstName = userInputFirstName;
        renameContact.LastName = userInputLastName;
        renameContact.Number = userPhoneNumber;


        var getContact = _contacts.FirstOrDefault(x => x.FirstName == renameContact.FirstName && x.LastName == renameContact.LastName && x.Number == renameContact.Number);

        return getContact;

    }

}

