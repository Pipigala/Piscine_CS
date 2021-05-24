using System;
using System.IO;

namespace ex01
{
    class Program
    {
        public static void NameShr(string userName)
        {
            string  libName;
            bool    flag;
            StreamReader file;

            flag = false;
            file = new StreamReader("text");
            while ((libName = file.ReadLine()) != null)
            {
                if (userName == libName)
                    flag = true;
            }
            if (flag)
                Console.WriteLine($"Hello, {userName}");
            file.Close();
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
