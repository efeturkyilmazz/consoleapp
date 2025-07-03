using System;

namespace HelloConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello Onftech!");

            
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            
            Console.WriteLine($"Welcome, {name}!");

            
            Console.Write("Enter your age: ");
            string ageInput = Console.ReadLine();

           
            if (int.TryParse(ageInput, out int age))
            {
                Console.WriteLine($"Nice to meet you, {name}. You are {age} years old.");
            }
            else
            {
                Console.WriteLine("Invalid age input!");
            }

           
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

