using static System.Console;
using static System.Math;

namespace task4
{
    class Program
    {
        static void Main()
        {
            Write("Enter x1: ");
            double x1 = double.Parse(ReadLine());
            double result = Pow(x1, 2) + Sin(x1);
            result = Round(result, 3);
            WriteLine("Result: {0}.", result);

            Write("Enter x2: ");
            double x2 = double.Parse(ReadLine());
            result = Sqrt(Pow(Cos(x2), 2) + Abs(x2));
            result = Round(result, 3);
            WriteLine("Result: {0}.", result);

            Write("Enter x3: ");
            double x3 = double.Parse(ReadLine());
            result = (1 / (x3 + 3)) - ((Pow(x3, 2) + 50) / 2);
            result = Round(result, 3);
            WriteLine("Result: {0}.", result);
        }
    }
}