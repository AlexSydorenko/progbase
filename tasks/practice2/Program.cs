using static System.Console;
using static System.Math;

namespace task5
{
    class Program
    {
        static void Main(string[] args)
        {
            // Task1
            Write("Enter integer number: ");
            int num = int.Parse(ReadLine());

            if (num > 0 && num <= 100)
            {
                WriteLine("Yes!");
            }
            else
            {
                WriteLine("No!");
            }

             // Task2
            Write("Enter integer number: ");
            int num = int.Parse(ReadLine());

            if (num % 2 == 0)
            {
                WriteLine("EVEN");
            }

            else
            {
                WriteLine("ODD");
            }

            // Task3
            Write("Enter integer number: ");
            int num = int.Parse(ReadLine());

            if (num % 3 == 0)
            {
                WriteLine("Yes!");
            }
            else
            {
                WriteLine("No!");
            }

            // Task6
            int a, b, c;
            Write("a = ");
            a = int.Parse(ReadLine());
            Write("b = ");
            b = int.Parse(ReadLine());
            Write("c = ");
            c = int.Parse(ReadLine());

            if (a + b < c || a + c < b || c + b < a)
            {
                WriteLine("Does not exist");
            }
            else if (a == b && b == c && a == c)
            {
                WriteLine("Equilateral");
            }
            else if (a == b || b == c || a == c)
            {
                WriteLine("Isosceles");
            }
            else
            {
                WriteLine("Arbitrary");
            }

            // Task7
            int mark;
            do
            {
                Write("Enter your mark: ");
                mark = int.Parse(ReadLine());
            }
            while (mark < 0 || mark > 100);

            if (mark < 60)
            {
                WriteLine("F");
            }
            else if (mark <= 64)
            {
                WriteLine("E");
            }
            else if (mark <= 74)
            {
                WriteLine("D");
            }
            else if (mark <= 84)
            {
                WriteLine("C");
            }
            else if (mark <= 94)
            {
                WriteLine("B");
            }
            else
            {
                WriteLine("A");
            }

            // ranges
            Write("Enter integer number: ");
            int num = int.Parse(ReadLine());

            if (num >= -5 && num <= 10)
            {
                WriteLine("True1");
            }
            else{
                WriteLine("False1");
            }

            if ((num >= -90 && num <= -50) || (num > -10 && num <= 20))
            {
                WriteLine("True2");
            }
            else
            {
                WriteLine("False2");
            }

            if ((num >= -80 && num <= 10) && (num > -20 && num <= 30))
            {
                WriteLine("True3");
            }
            else
            {
                WriteLine("False3");
            }

            // function
            Write("x = ");
            double x = double.Parse(ReadLine());
            double y;

            if (x >= 0)
            {
                y = Sin(Pow(x, 3));
            }

            else
            {
                y = Sin(x - PI);
            }
            WriteLine("y = " + y);
        }
    }
}