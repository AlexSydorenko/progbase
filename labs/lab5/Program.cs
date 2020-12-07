using System;
using System.IO;

namespace lab5
{
    struct Course
    {
        public int id;
        public string name;
        public string group;
        public int semester;
    }

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter command: ");
                string command = Console.ReadLine();
                string[] subcommands = command.Split(' ');
                
                if (subcommands[0] == "char")
                {
                    ProcessChar(subcommands);
                }
                else if (subcommands[0] == "string")
                {
                    ProcessString(subcommands);
                }
                else if (subcommands[0] == "csv")
                {
                    ProcessCsv(subcommands);
                }
                else if (subcommands[0] == "exit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Unknown command: `{0}`", subcommands[0]);
                    break;
                }
            }
            Console.WriteLine("Bye.");
        }

        static void ProcessChar(string[] subcommands)
        {
            if (subcommands.Length != 2)
            {
                Console.WriteLine("Error: should have 2 parts in char commands, but have {0}", subcommands.Length);
                return;
            }
            if (subcommands[1] == "all")
            {
                PrintCharAll();
            }
            else if (subcommands[1] == "upper")
            {
                PrintCharUpper();
            }
            else if (subcommands[1] == "alpha")
            {
                PrintCharAlpha();
            }
            else if (subcommands[1] == "punct")
            {
                PrintCharPunct();
            }
            else
            {
                Console.WriteLine("Error: unknown char command `{0}`", subcommands[1]);
            }
        }

        static void PrintCharAll()
        {
            for (int code = 0; code <= 127; code++)
            {
                Console.WriteLine("{0} `{1}`", code, (char)code);
            }
        }

        static void PrintCharUpper()
        {
            for (int code = 0; code <= 127; code++)
            {
                if (char.IsUpper((char)code))
                {
                    Console.WriteLine("{0} `{1}`", code, (char)code);
                }
            }
        }

        static void PrintCharAlpha()
        {
            for (int code = 0; code <= 127; code++)
            {
                if (char.IsLetter((char)code))
                {
                    Console.WriteLine("{0} `{1}`", code, (char)code);
                }
            }
        }

        static void PrintCharPunct()
        {
            for (int code = 0; code <= 127; code++)
            {
                if (char.IsPunctuation((char)code))
                {
                    Console.WriteLine("{0} `{1}`", code, (char)code);
                }
            }
        }
    

        static string task2String = "";

        static void ProcessString(string[] subcommands)
        {
            if (subcommands.Length < 2)
            {
                Console.WriteLine("Error: should have at less 2 parts in string commands, but have {0}", subcommands.Length);
                return;
            }
            if (subcommands.Length == 2)
            {
                if (subcommands[1] == "print")
                {
                    PrintStringPrint();
                }
                else if (subcommands[1] == "upper")
                {
                    PrintStringUpper();
                }
                else
                {
                    Console.WriteLine("Error: unknown string command `{0}`", subcommands[1]);
                }
            }
            else if (subcommands.Length > 2)
            {
                if (subcommands[1] == "set")
                {
                    PrintStringSet(subcommands);
                }
                else if (subcommands[1] == "substr")
                {
                    if (subcommands.Length != 4)
                    {
                        Console.WriteLine("Error: Substr command should consists of 4 parts");
                    }
                    string indexString = subcommands[2];
                    string lengthString = subcommands[3];
                    int index = int.Parse(indexString);
                    int length = int.Parse(lengthString);
                    PrintStringSubstr(index, length);
                }
                else if (subcommands[1] == "contains")
                {
                    PrintStringContains(subcommands);
                }
                else
                {
                    Console.WriteLine("Error: unknown string command `{0}`", subcommands[1]);
                }
            }
        }

        static void PrintStringPrint()
        {
            Console.WriteLine("String: {0}", task2String);
            Console.WriteLine("Number of characters: {0}", task2String.Length);
        }

        static void PrintStringSet(string[] subcommands)
        {
            for (int i = 2; i < subcommands.Length; i++)
            {
                task2String += subcommands[i] + " ";
            }
            Console.WriteLine("Your new string: `{0}`", task2String);
        }

        static void PrintStringSubstr(int startIndex, int length)
        {
            if (startIndex < 0 || startIndex > task2String.Length)
            {
                Console.WriteLine("Error: Substr index is out of range");
                return;
            }
            if (length < 0 || length > task2String.Length)
            {
                Console.WriteLine("Error: Substr length is out of range");
                return;
            }
            string s = task2String.Substring(startIndex, length);
            Console.WriteLine("Substring is `{0}`", s);
        }

        static void PrintStringUpper()
        {
            Console.WriteLine("String with all upper letters: `{0}`", task2String.ToUpper());
        }

        static void PrintStringContains(string[] subcommands)
        {
            string substring = "";
            for (int i = 2; i < subcommands.Length; i++)
            {
                substring += subcommands[i];
            }
            bool contains = task2String.Contains(substring);
            if (contains)
            {
                Console.WriteLine("True");
            }
            else
            {
                Console.WriteLine("False");
            }
        }
    

        static string task3CsvText = "";
        static string[,] task3Table = new string[20, 20];
        static Course[] task3Courses = new Course[0];

        static void ProcessCsv(string[] subcommands)
        {
            if (subcommands.Length < 2)
            {
                Console.WriteLine("Error: should have at less 2 parts in csv commands, but have {0}", subcommands.Length);
                return;
            }
            if (subcommands.Length == 2)
            {
                if (subcommands[1] == "load")
                {
                    task3CsvText = File.ReadAllText("./data.csv");
                    CsvToTable(task3CsvText);
                }
                else if (subcommands[1] == "text")
                {
                    Console.WriteLine();
                    ProcessCsvText();
                    Console.WriteLine();
                }
                else if (subcommands[1] == "table")
                {
                    Console.WriteLine();
                    ProcessCsvTable(task3Table);
                }
                else if (subcommands[1] == "courses")
                {
                    Console.WriteLine();
                    ProcessCsvCourses();
                    Console.WriteLine();
                }
                else if (subcommands[1] == "save")
                {
                    ProcessCsvSave();
                }
                else
                {
                    Console.WriteLine("Error: unknown csv command `{0}`", subcommands[1]);
                }
            }
            else if (subcommands.Length > 2)
            {
                if (subcommands[1] == "get")
                {
                    if (subcommands.Length != 3)
                    {
                        Console.WriteLine("Error: Get command should consists of 3 parts, but have: `{0}`", subcommands.Length);
                    }
                    else
                    {
                        string indexString = subcommands[2];
                        int index = int.Parse(indexString);
                        Console.WriteLine();
                        ProcessCsvGet(index);
                        Console.WriteLine();
                    }
                }
                else if (subcommands[1] == "set")
                {
                    string indexString = subcommands[2];
                    int index = int.Parse(indexString);
                    string field = subcommands[3];
                    ProcessCsvSet(index, field, subcommands);
                }
                else
                {
                    Console.WriteLine("Error: unknown csv command `{0}`", subcommands[1]);
                }
            }
        }

        static void ProcessCsvText()
        {
            Console.WriteLine(task3CsvText);
        }

        static void ProcessCsvTable(string[,] table)
        {
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    Console.Write(table[i, j] + " ");
                }
            }
            Console.WriteLine();
        }

        static void ProcessCsvCourses()
        {
            string[] lines=task3CsvText.Split('\r');
            for (int i = 1; i < lines.Length; i++)
            {
                string[] sublines=lines[i].Split(',');
                Console.WriteLine("ID: {0}; Name: {1}; Group: {2}; Semester: {3}", TableToCourses(sublines).id, TableToCourses(sublines).name,
                                                                                TableToCourses(sublines).group, TableToCourses(sublines).semester);
            }
        }

        static void ProcessCsvGet(int index)
        {
            if (index < 1 || index > task3Table.GetLength(0))
            {
                Console.WriteLine("Error: Index is out of range!", task3Table.GetLength(1));
                return;
            }
            if (task3CsvText != "")
            {
                string[] lines = task3CsvText.Split('\r');
                string[] sublines = lines[index].Split(',');
                Console.WriteLine("ID: {0}; Name: {1}; Group: {2}; Semester: {3}", TableToCourses(sublines).id, TableToCourses(sublines).name,
                                                                                TableToCourses(sublines).group, TableToCourses(sublines).semester);
            }
        }

        static void ProcessCsvSet(int index, string field, string[] subcommands)
        {
            string[] lines = task3CsvText.Split('\r');
            string[] sublines = lines[0].Split(','); 
            string newValue = "";
            for (int i = 4; i < subcommands.Length; i++)
            {
                newValue += subcommands[i] + " ";
            }
            if (newValue.Contains(',') || newValue.Contains('"'))
            {
                Console.WriteLine("Error: Incorrect input! Input should not contain comma or double quotes.");
                return;
            }
            else
            {
                if (field == "id")
                {
                    task3Table[index, 0] = newValue;
                }
                else if (field == "name")
                {
                    task3Table[index, 1] = newValue;
                }
                else if (field == "group")
                {
                    task3Table[index, 2] = newValue;
                }
                else if (field == "semester")
                {
                    task3Table[index, 3] = newValue;
                }
                else
                {
                    Console.WriteLine("Error: unknown csv set command: `{0}`", field);
                    return;
                }
            }
            Console.WriteLine("A value was successfully changed on: `{0}`", newValue);
        }

        static void ProcessCsvSave()
        {
            string[] lines = task3CsvText.Split('\r');
            string[] subcount = lines[0].Split(',');
            string csvSave = "";
            for (int i = 0; i <= lines.Length-2; i++)
            {
                for (int j = 0; j < subcount.Length; j++)
                {
                    if (j == subcount.Length-1)
                    {
                        csvSave += task3Table[i, j] + "\r";
                    }
                    else
                    {
                        csvSave += task3Table[i, j] + ",";
                    }
                    
                }
            }
            File.WriteAllText("./data.csv", csvSave);
        }

        static string[,] CsvToTable(string csvText)
        {
            string[] lines = task3CsvText.Split('\r');
            for (int i = 0; i < lines.Length; i++)
            {
                string[] sublines=lines[i].Split(',');

                for (int j = 0; j < sublines.Length; j++)
                {
                    task3Table[i,j] = sublines[j];
                }
            }
            return task3Table;
        }

        static Course TableToCourses(string[] line)
        {
            Course C = new Course();
            C.id = int.Parse(line[0]);
            C.name = line[1];
            C.group = line[2];
            C.semester = int.Parse(line[3]);
            return C;
        }
    }
}
