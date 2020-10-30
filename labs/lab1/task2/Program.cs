using static System.Console;
using static System.Math;

namespace task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("x = ");
            double x = int.Parse(ReadLine());
            double y = 0;

            if ((x >= -4.5 && x <= 4.5) || (x > 5 && x < 10))
            {
                y = Tan(Pow(x, 2)) + Pow(Sin(2 * x), 2);
            }
            else
            {
                y = Pow(x, 2) - 5 / (x - 1);
            }

            if (x == 1)
            {
                y = double.NaN;
            }

            WriteLine("x = {0}", x);
            WriteLine("y = {0}", y);
        }
    }
}