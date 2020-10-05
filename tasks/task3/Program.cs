using System;

namespace task3
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter your number:");
            int num = int.Parse(Console.ReadLine());

            int digit1 = num % 10;
            int digit2 = (num / 10) % 10;
            int digit3 = num / 100;

            int sum = digit1 + digit2 + digit3;
            Console.WriteLine("The sum is {0}.", sum);
        }
    }
}