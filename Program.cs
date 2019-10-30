using System;
using SemearTest.Entities;

namespace SemearTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Merchant's Guide to the Galaxy");
            Console.WriteLine("Please make your question: ");

            String question = Console.ReadLine();

            Question questionModel = new Question(question);

            String response = questionModel.Process(question);

            Console.WriteLine(question);
        }

        
    }
}
