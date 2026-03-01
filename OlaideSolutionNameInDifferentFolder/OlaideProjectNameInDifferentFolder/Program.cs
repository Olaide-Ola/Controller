using OlaideSecondProjectName.SecondProjectFolder;
using ClassLibraryThird;

namespace OlaideProjectNameInDifferentFolder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello First Project");
            FolderClass folderClass = new FolderClass();
            folderClass.Show();


            Class1 class1 = new Class1();
            class1.DisplayTest();
        }
    }
}
