using System;

namespace task2
{
    class Program
    {
        static void Main()
        {
            int a, b, c;
            Console.WriteLine("Enter number a:");
            a = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter number b:");
            b = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter number c:");
            c = int.Parse(Console.ReadLine());

            int sum = a + b + c;
            double avg = sum / 3;

            Console.WriteLine("Averege: {0}.", avg);
        }
    }
}