using static System.Console;
using static System.Math;

class Program
{
    static void Main(string[] args)
    {
        Write("a = ");
        double a = double.Parse(ReadLine());
        Write("b = ");
        double b = double.Parse(ReadLine());
        Write("c = ");
        double c = double.Parse(ReadLine());

        double d0 = (Pow(a+3, c+1) - 10) / (a-b);
        double d1 = b / (13 * Abs(a+b));
        double d2 = Pow(a+7, Abs(Sin(b)) / (1+c));
        double d = d0 + d1 + d2;

        WriteLine("d0 = {0}", d0);
        WriteLine("d1 = {0}", d1);
        WriteLine("d2 = {0}", d2);
        WriteLine("d = {0}", d);
    }
}
