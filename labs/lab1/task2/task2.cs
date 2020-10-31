using static System.Console;
using static System.Math;

class Program
{
    static void Main(string[] args)
    {
        Write("x = ");
        double x = int.Parse(ReadLine());
        double y = 0;

        if (Cos(Pow(x, 2)) == 0)
        {
            y = double.NaN;
        }
        else if ((x >= -4.5 && x <= 4.5) || (x > 5 && x < 10))
        {
            y = Tan(Pow(x, 2)) + Pow(Sin(2 * x), 2);
        }
        else
        {
            y = Pow(x, 2) - 5 / (x - 1);
        }

        WriteLine("x = {0}", x);
        WriteLine("y = {0}", y);
    }
}