using System;
using static System.Math;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Натисніть 1, щоб виконати перше завдання, або 2 - для виконання другого");
        int ChosenVariant = int.Parse(Console.ReadLine());
        if (ChosenVariant == 1)
        {
            //Part 1
            int[] OrigArray = new int[] {-3, -4, 3, 6, 0, 3, -1, 6, -2, -4, -3};
            int[] ModifiedArray = ShiftHeights(OrigArray);
            // Console.WriteLine("Масив зміщених висот:");
            // PrintArray(ModifiedArray);
            // Console.WriteLine();

            int WaterLvl = 0;
            do
            {
                Console.Write("Введіть висоту рівня води: ");
                WaterLvl = int.Parse(Console.ReadLine());
            }
            while (WaterLvl < 0 || WaterLvl > GetMaxValue(ModifiedArray));
            int[] WaterHeights = GetWaterHeights(ModifiedArray, WaterLvl);
            // Console.WriteLine("Висоти стовпців води:");
            // PrintArray(WaterHeights);
            // Console.WriteLine();

            int WaterVolume = CountWaterVolume(WaterHeights);
            Console.WriteLine("Об'єм води: {0} куб. м.", WaterVolume);
            Console.WriteLine();

            PrintLandAndWater(ModifiedArray, WaterLvl);
        }
        else if (ChosenVariant == 2)
        {
            //Part 2
            int[,] OrigIslands = new int[,] {
                {1, 0, 1, 0, 0, 0, 0, 0, 1},
                {1, 0, 1, 0, 0, 0, 0, 0, 1},
                {0, 0, 1, 0, 0, 1, 1, 1, 1},
                {0, 0, 0, 0, 0, 1, 1, 0, 0},
                {1, 1, 0, 0, 0, 0, 0, 1, 0},
                {1, 1, 1, 1, 1, 0, 0, 1, 0},
                {1, 1, 1, 0, 1, 1, 0, 1, 1},
                {0, 0, 1, 1, 0, 0, 1, 0, 0},
                {0, 1, 0, 0, 1, 1, 1, 0, 0},
                {1, 0, 0, 1, 1, 1, 0, 0, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1}
            };
            int[,] InvertedIslands = InvertMatrix(OrigIslands);
            int[,] ModifiedIslands = ModifyMatrix(InvertedIslands);
            int[,] AdditionalMatrixWithZeros = CreateAdditionalMatrix(ModifiedIslands);
            int[] counters = new int[CountNonZeros(InvertedIslands)];
            MergeOnes(AdditionalMatrixWithZeros, counters);
            CreateCounters(counters, AdditionalMatrixWithZeros);

            // for (int i = 0; i < AdditionalMatrixWithZeros.GetLength(0); i++)
            // {
            //     for (int j = 0; j < AdditionalMatrixWithZeros.GetLength(1); j++)
            //     {
            //         Console.Write(AdditionalMatrixWithZeros[i, j] + " ");
            //     }
            //     Console.WriteLine();
            // }

            // PrintArray(counters);

            int VolumeOfTheBiggestWaterZone = CountVolumeOfTheBiggestWaterZone(counters);
            Console.WriteLine("Об'єм найбільшого резервуару води: {0} куб. м.", VolumeOfTheBiggestWaterZone);
            Console.WriteLine();

            PrintIslands(OrigIslands);
        }
        else
        {
            Console.WriteLine("Помилка! Переконайтесь, що ввели правильні дані");
        }
    }

    //Part1 (functions)
    static void PrintArray(int[] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();
    }

    static int[] ShiftHeights(int[] array)
    {
        int[] ModifiedArray = new int[array.GetLength(0)];
        int MinValue = GetMinValue(array);
        for (int i = 0; i < array.GetLength(0); i++)
        {
            array[i] = array[i] + Abs(MinValue);
        }
        return array;
    }

    static int GetMinValue(int[] array)
    {
        int MinValue = 0;
        for (int i = 0; i < array.GetLength(0); i++)
        {
            if (array[i] < MinValue)
            {
                MinValue = array[i];
            }
        }
        return MinValue;
    }

    static int GetMaxValue(int[] array)
    {
        int MaxValue = 0;
        for (int i = 0; i < array.GetLength(0); i++)
        {
            if (array[i] > MaxValue)
            {
                MaxValue = array[i];
            }
        }
        return MaxValue;
    }

    static int[] GetWaterHeights(int[] heights, int waterLevel)
    {
        int[] waterHeights = new int[heights.GetLength(0)];
        for (int i = 0; i < heights.GetLength(0); i++)
        {
            waterHeights[i] = waterLevel - heights[i];
            if (waterHeights[i] < 0)
            {
                waterHeights[i] = 0;
            }
        }
        return waterHeights;
    }

    static int CountWaterVolume(int[] heights)
    {
        int waterVolume = 0;
        for (int i = 0; i < heights.GetLength(0); i++)
        {
            waterVolume += heights[i];
        }
        return waterVolume;
    }

    static void PrintLandAndWater(int[] array, int waterLevel)
    {
        for (int i = 0; i < array.GetLength(0)+2; i++)
        {
            Console.Write("-");
        }
        Console.WriteLine(" " + (GetMaxValue(array)+1));
        for (int i = GetMaxValue(array); i > 0; i--)
        {
            Console.Write("|");
            for (int j = 0; j < array.GetLength(0); j++)
            {
                if (array[j] < i)
                {
                    if (i > waterLevel)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("~");
                    }
                }
                else
                {
                    Console.Write("N");
                }
            }
            Console.Write("|");
            if (i == waterLevel)
            {
                Console.WriteLine(" " + i + " (water level)");
            }
            else
            {
                Console.WriteLine(" " + i);
            }
        }
        for (int i = 0; i < array.GetLength(0)+2; i++)
        {
            Console.Write("-");
        }
        Console.WriteLine(" " + 0);
    }

    //Part2 (functions)
    static int[,] InvertMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == 0)
                {
                    matrix[i, j] = 1;
                }
                else
                {
                    matrix[i, j] = 0;
                }
            }
        }
        return matrix;
    }
    
    static int CountNonZeros(int[,] matrix)
    {
        int counter = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] != 0)
                {
                    counter++;
                }
            }
        }
        counter++;
        return counter;
    }

    static int[,] ModifyMatrix(int[,] array)
    {
        int counter = 0;
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j] != 0)
                {
                    array[i, j] += counter;
                    counter++;
                }
            }
        }
        return array;
    }

    static int[] CreateCounters(int[] array, int[,] matrix)
    {
        for (int k = 1; k < array.GetLength(0); k++)
        {
            array[k] = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (k == matrix[i, j])
                    {
                        array[k]++;
                    }
                }
            }
        }
        return array;
    }

    static int[,] CreateAdditionalMatrix(int[,] matrix)
    {
        int[,] NewMatrix = new int[matrix.GetLength(0)+2, matrix.GetLength(1)+2];
        for (int j = 0; j < matrix.GetLength(0) + 2; j++)
        {
            NewMatrix[j, 0] = 0;
        }
        for (int i = 1; i < matrix.GetLength(1)+1; i++)
        {
            NewMatrix[0, i] = 0;
        }
        for (int i = 1; i < matrix.GetLength(0); i++)
        {
            for (int j = 1; j < matrix.GetLength(1); j++)
            {
                NewMatrix[i, j] = matrix[i-1, j-1];
            }
        }
        for (int i = 1; i < matrix.GetLength(1)+1; i++)
        {
            NewMatrix[matrix.GetLength(0)+1, i] = 0;
        }
        for (int j = 1; j < matrix.GetLength(0)+1; j++)
        {
            NewMatrix[j, matrix.GetLength(1)+1] = 0;
        }
        return NewMatrix;
    }

    static int[] MergeOnes(int[,] matrix, int[] counters)
    {
        int changes = 0;
        do
        {
            changes = 0;
            for (int i = 1; i < matrix.GetLength(0)-1; i++)
            {
                for (int j = 1; j < matrix.GetLength(1)-1; j++)
                {
                    if (matrix[i, j] != 0)
                    {
                        if (matrix[i, j-1] != 0)
                        {
                            if (matrix[i, j] > matrix[i, j-1])
                            {
                                matrix[i, j] = matrix[i, j-1];
                                counters[matrix[i, j]]--;
                                counters[matrix[i, j-1]]++;
                                changes++;
                            }
                        }
                        if (matrix[i, j+1] != 0)
                        {
                            if (matrix[i, j] > matrix[i, j+1])
                            {
                                matrix[i,j] = matrix[i, j+1];
                                counters[matrix[i, j]]--;
                                counters[matrix[i, j+1]]++;
                                changes++;
                            }
                        }
                        if (matrix[i-1, j] != 0)
                        {
                            if (matrix[i, j] > matrix[i-1, j])
                            {
                                matrix[i, j] = matrix[i-1, j];
                                counters[matrix[i, j]]--;
                                counters[matrix[i-1, j]]++;
                                changes++;
                            }
                        }
                        if (matrix[i+1, j] != 0)
                        {
                            if (matrix[i, j] > matrix[i+1, j])
                            {
                                matrix[i, j] = matrix[i+1, j];
                                counters[matrix[i, j]]--;
                                counters[matrix[i+1, j]]++;
                                changes++;
                            }
                        }
                    }
                }
            }
        }
        while (changes != 0);
        return counters;
    }

    static int CountVolumeOfTheBiggestWaterZone(int[] array)
    {
        int theBiggestWaterZone = 0;
        for (int i = 0; i < array.GetLength(0); i++)
        {
            if (array[i] > theBiggestWaterZone)
            {
                theBiggestWaterZone = array[i];
            }
        }
        return theBiggestWaterZone;
    }

    static void PrintIslands(int[,] matrix)
    {
        Console.Write("+");
        for (int i = 0; i < matrix.GetLength(0)-2; i++)
        {
            Console.Write("-");
        }
        Console.WriteLine("+");
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            Console.Write("|");
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] != 0)
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write("N");
                }
            }
            Console.WriteLine("|");
        }
        Console.Write("+");
        for (int i = 0; i < matrix.GetLength(0)-2; i++)
        {
            Console.Write("-");
        }
        Console.WriteLine("+");
    }
}
