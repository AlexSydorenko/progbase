using System;
using Progbase.Procedural;
using static System.Math;

namespace lab4
{
    class Program
    {
        struct Point
        {
            public double x;
            public double y;
        }

        struct Circle
        {
            public Point center;
            public double radius;
        }

        static void Main(string[] args)
        {
            const int size = 40;
            //
            Circle c = new Circle();
            c.center = new Point();
            c.center.x = size / 2;
            c.radius = 5;
            int y0 = size / 2;
            double s1 = 10;
            double s2 = 0.5;
            //init
            Canvas.SetSize(size, size);
            Canvas.InvertYOrientation();
            //
            Console.Clear();
            ConsoleKeyInfo keyInfo;
            do
            {
                // 1. dep. objects update
                c.center.y = s1 * Sin(s2 * c.center.x) + y0;
                // 2. draw
                Canvas.BeginDraw();

                Canvas.SetColor(255, 255, 255);
                Canvas.StrokeLine(0, y0, size, y0);

                Canvas.SetColor(0, 255, 0);
                Canvas.FillCircle((int)c.center.x, (int)c.center.y, (int)c.radius);

                Canvas.SetColor(0, 0, 255);
                Canvas.PutPixel((int)c.center.x, (int)c.center.y);

                Canvas.EndDraw();
                // 3. user input
                Console.WriteLine();
                Console.Write("Press Esc to stop the program! ");
                keyInfo = Console.ReadKey();
                // 4. update

                // move circle
                if (keyInfo.Key == ConsoleKey.A)
                {
                    if (c.center.x > 0)
                    {
                        c.center.x -= 1;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.D)
                {
                    if (c.center.x < size -1)
                    {
                        c.center.x += 1;
                    }
                }
                // change circle radius
                else if (keyInfo.Key == ConsoleKey.Q)
                {
                    if (c.radius > 1)
                    {
                        c.radius -= 1;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.E)
                {
                    c.radius += 1;
                }
                // change amplitude
                else if (keyInfo.Key == ConsoleKey.S)
                {
                    if (s1 > 0)
                    {
                        s1 -= 0.5;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.W)
                {
                    s1 += 0.5;
                }
                // change period
                else if (keyInfo.Key == ConsoleKey.Z)
                {
                    if (s2 > 0)
                    {
                        s2 -= 0.1;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.C)
                {
                    s2 += 0.1;
                }
            }
            while (keyInfo.Key != ConsoleKey.Escape);

            Console.WriteLine();
        }
    }
}
