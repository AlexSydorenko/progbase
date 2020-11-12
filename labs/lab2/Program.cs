using static System.Console;
using static System.Math;

class Program
{
    static void Main()
    {
        double y = 0;
        for (double x = -10; x <= 10; x += 0.5)
        {
            y = Fx(x);
            WriteLine("y({0}) = {1}", x, y);
        }
        WriteLine();

        Write("xMin: ");
        double xMin = double.Parse(ReadLine());
        Write("xMax: ");
        double xMax = double.Parse(ReadLine());
        Write("nSteps: ");
        int nSteps = int.Parse(ReadLine());
        bool breakPoint = Period(xMax, xMin);

        if (xMin >= xMax || nSteps <= 0 || breakPoint == true)
        {
            WriteLine("Incorrect data!");
        }
        else
        {
            double integral = IntFx(xMin, xMax, nSteps);
            WriteLine("Integral = {0}", integral);
        }
    }

    static double Gx(double x)
    {
        double y;
        if (Cos(Pow(x, 2)) == 0)
        {
            y = double.NaN;
        }
        else
        {
            y = Tan(Pow(x, 2)) + Pow(Sin(2 * x), 2);
        }
        return y;
    }

    static double Hx(double x)
    {
        double y;
        if (x == 1)
        {
            y = double.NaN;
        }
        else
        {
            y = Pow(x, 2) - 5 / (x - 1);
        }
        return y;
    }

    static double Fx(double x)
    {
        double y;
        if ((x >= -4.5 && x < -1) || (x > 1 && x <=4.5))
        {
            y = Gx(x);
        }
        else
        {
            y = Hx(x);
        }
        return y;
    }

    static double IntFx(double xMin, double xMax, int nSteps)
    {
        double step = (xMax - xMin) / nSteps;
        double sum = 0;
        for (double i = 1; i <= nSteps; i++)
        {
            double x = xMin + i * step;
            sum += Fx(x);
        }
        double result = step * sum;
        return result;
    }

    static bool Period(double xMax, double xMin)
    {
        int n = (int)Ceiling((Pow(xMin, 2) / PI) + 0.5);
        double xn = Sqrt((0.5 + n) * PI);
        return xn <= xMax;
    }
}