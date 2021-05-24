using System;
using System.IO;

namespace ex01
{
    class Program
    {
        public static int EditDist(string libName, string userName)
        {
            int i;
            int c;
            int edCount;
            char[] copy;

            i = 0;
            c = 0;
            edCount = 0;            
            copy = new char[libName.Length];
            while (i < libName.Length && i < userName.Length)
            {
                copy[i] = libName[i];
                if (libName[i] != userName[i - c])
                {
                   edCount++;
                   c++;
                }
                i++;
            }
            edCount = edCount + ((libName.Length > userName.Length) ? libName.Length - userName.Length : userName.Length - libName.Length);
            userName = libName;
            return (edCount);
        }
        public static void NameShr(string userName)
        {
            string  libName;
            StreamReader file;
            string    uns;

            uns = null;
            file = new StreamReader("text");
            while ((libName = file.ReadLine()) != null)
            {
                if (userName == libName)
                {
                    Console.WriteLine($">Hello, {userName}");
                    return;
                }
            }
            file.Close();
            file = new StreamReader("text");
            while ((libName = file.ReadLine()) != null)
            {
                if (EditDist(libName, userName) < 3 && libName[0] == userName[0])
                {
                    Console.WriteLine($">Вы имели ввиду \"{libName}\"?Y/N");
                    uns = Console.ReadLine();
                    if (uns == "y" || uns == "Y")
                    {
                        Console.WriteLine($">Hello, {libName}");
                        return;
                    }
                }
            }
            file.Close();
            Console.WriteLine("Your name was not found.");
        }
        static void Main(string[] args)
        {
            string userName;
            Console.WriteLine(">Enter name:");
            userName = Console.ReadLine();
            NameShr(userName);
        }
    }
}
