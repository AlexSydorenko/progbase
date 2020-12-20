using System;
using System.Diagnostics;
using System.IO;

namespace lab6
{
    struct Options
    {
        public bool isInteractiveMode;  // for -i boolean option
        public string inputFile;  // for the independent option
        public string outputFile;  // for -o value option
        // for errors
        public string parsingError;
    }

    class Program
    {
        enum State
        {
            Undefined,
            AtSign,
            MainPart
        }

        static void Main(string[] args)
        {
            RunTests();

            Options options = ParseOptions(args);
            if (options.isInteractiveMode == false)
            {
                // Console.WriteLine("Command Line Arguments ({0}):", args.Length);
                // for (int i = 0; i < args.Length; i++)
                // {
                //     Console.WriteLine("[{0}] \"{1}\"", i, args[i]);
                // }
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("You are in STANDART mode.");
                Console.ResetColor();
                Console.WriteLine();
                if (options.parsingError != "")
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"ERROR: {options.parsingError}");
                    Console.ResetColor();
                    Environment.Exit(1);
                }
                else if (options.inputFile != File.ReadAllText("./inputFile.txt"))
                {
                    Console.WriteLine("Options: ");
                    Console.WriteLine($"Interactive: {options.isInteractiveMode}");
                    Console.WriteLine($"Input: {options.inputFile}");
                    Console.WriteLine($"Output: {options.outputFile}");
                }
                else
                {
                    Console.WriteLine("Options: ");
                    Console.WriteLine($"Interactive: {options.isInteractiveMode}");
                    Console.WriteLine($"Input: {options.inputFile}");
                    Console.WriteLine("Array of identifiers:");
                    PrintArray(GetAllIdentifiers(options.inputFile));
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("You are in INTERACTIVE mode");
                Console.ResetColor();
                Console.WriteLine();
                while (true)
                {
                    Console.Write("Enter command: ");
                    string command = Console.ReadLine();
                    if (command == "exit")
                    {
                        break;
                    }
                    if (command == "")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("ERROR: empty input!");
                        Console.ResetColor();
                        break;
                    }

                    Console.WriteLine($"1. Is input true or false: '{CheckIdentifiers(command)}'");
                    Console.WriteLine($"2. Number of identifiers: '{CountIdentifiers(command)}'");
                    Console.WriteLine("3. Array of identifiers: ");
                    PrintArray(GetAllIdentifiers(command));
                    Console.WriteLine();

                }
                Console.WriteLine("Bye!");
                Console.WriteLine();
            }
        }

        // Interactive mode
        static void RunTests()
        {
            // Func #1 (part 1)
            Debug.Assert(CheckIdentifiers("@asdasda,") == true);
            Debug.Assert(CheckIdentifiers("@asdasda,@1") == false);
            Debug.Assert(CheckIdentifiers("@asdasda,@sca111,") == true);
            Debug.Assert(CheckIdentifiers("@asdasda@cacsac,") == false);

            // Func #2 (part 1)
            Debug.Assert(CountIdentifiers("@3abc.") == 0);
            Debug.Assert(CountIdentifiers("@abc.") == 1);
            Debug.Assert(CountIdentifiers("@abc@_123,@Hello ") == 2);
            Debug.Assert(CountIdentifiers("@.") == 0);

            // Func #3 (part 1)
            Debug.Assert(CompareArrays(GetAllIdentifiers("@abc @qwerty."), new string[]{"@abc", "@qwerty"}));
            Debug.Assert(CompareArrays(GetAllIdentifiers("@5bc/"), new string[0]{}));
            Debug.Assert(CompareArrays(GetAllIdentifiers("@_bc@qwerty."), new string[]{"@qwerty"}));
            Debug.Assert(CompareArrays(GetAllIdentifiers("@@@qwerty////@abc."), new string[]{"@qwerty", "@abc"}));

            // Part 2
            Debug.Assert(CompareOptions(ParseOptions(new string[]{}), new Options()
            {
                isInteractiveMode = false,
                inputFile = "",
                outputFile = "",
                parsingError = "",
            }));

            Debug.Assert(CompareOptions(ParseOptions(new string[]{"-i"}), new Options()
            {
                isInteractiveMode = true,
                inputFile = "",
                outputFile = "",
                parsingError = "",
            }));

            Debug.Assert(CompareOptions(ParseOptions(new string[]{"-o", "example.txt"}), new Options()
            {
                isInteractiveMode = false,
                inputFile = "",
                outputFile = "example.txt",
                parsingError = "",
            }));

            Debug.Assert(CompareOptions(ParseOptions(new string[]{"example.txt"}), new Options()
            {
                isInteractiveMode = false,
                inputFile = "example.txt",
                outputFile = "",
                parsingError = "",
            }));

            Debug.Assert(CompareOptions(ParseOptions(new string[]{"-i", "example.txt", "-o", "out"}), new Options()
            {
                isInteractiveMode = true,
                inputFile = "example.txt",
                outputFile = "out",
                parsingError = "",
            }));

            Debug.Assert(CompareOptions(ParseOptions(new string[]{"-o", "out", "-i", "example.txt"}), new Options()
            {
                isInteractiveMode = true,
                inputFile = "example.txt",
                outputFile = "out",
                parsingError = "",
            }));

            Debug.Assert(ParseOptions(new string[]{"-o"}).parsingError != "");
            Debug.Assert(ParseOptions(new string[]{"-o -i"}).parsingError != "");
            Debug.Assert(ParseOptions(new string[]{"-K"}).parsingError != "");
        }

        static bool CompareArrays(string[] arr1, string[] arr2)
        {
            if (arr1.Length != arr2.Length)
            {
                return false;
            }
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i])
                {
                    return false;
                }
            }
            return true;
        }

        static bool CheckIdentifiers(string command)
        {
            State state = State.Undefined;
            for (int i = 0; i < command.Length; i++)
            {
                char c = command[i];
                if (state == State.Undefined)
                {
                    if (c == '@')
                    {
                        state = State.AtSign;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (state == State.AtSign)
                {
                    if (c == '_' || char.IsLetter(c))
                    {
                        state = State.MainPart;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (state == State.MainPart)
                {
                    if ((char.IsPunctuation(c) || char.IsSeparator(c)) && (c != '@' && c != '_'))
                    {
                        state = State.Undefined;
                    }
                    else if (!char.IsLetterOrDigit(c) && c != '_')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static int CountIdentifiers(string command)
        {
            int counter = 0;
            State state = State.Undefined;
            for (int i = 0; i < command.Length; i++)
            {
                char c = command[i];
                if (state == State.Undefined)
                {
                    if (c == '@')
                    {
                        state = State.AtSign;
                    }
                }
                else if (state == State.AtSign)
                {
                    if (c == '_' || char.IsLetter(c))
                    {
                        state = State.MainPart;
                    }
                    else
                    {
                        state = State.Undefined;
                    }
                }
                else if (state == State.MainPart)
                {
                    if (c == '@')
                    {
                        state = State.AtSign;
                    }
                    else if ((char.IsPunctuation(c) || char.IsSeparator(c)) && c != '_')
                    {
                        counter++;
                        state = State.Undefined;
                    }
                }
            }

            return counter;
        }

        static string[] GetAllIdentifiers(string command)
        {
            string[] identifiers = new string[CountIdentifiers(command)];
            State state = State.Undefined;
            string identifier = "";
            int index = 0;
            for (int i = 0; i < command.Length; i++)
            {
                char c = command[i];
                if (state == State.Undefined)
                {
                    if (c == '@')
                    {
                        identifier += c;
                        state = State.AtSign;
                    }
                }
                else if (state == State.AtSign)
                {
                    if (c == '_' || char.IsLetter(c))
                    {
                        identifier += c;
                        state = State.MainPart;
                    }
                    else
                    {
                        identifier = "";
                        state = State.Undefined;
                    }
                }
                else if (state == State.MainPart)
                {
                    if (c == '_' || char.IsLetterOrDigit(c))
                    {
                        identifier += c;
                    }
                    else if ((char.IsPunctuation(c) || char.IsSeparator(c)))
                    {
                        if (identifier.Length > 1 && (c != '@' && c != '_'))
                        {
                            identifiers[index] = identifier;
                            identifier = "";
                            index++;
                        }
                        else if (c == '@')
                        {
                            identifier = "@";
                            state = State.AtSign;
                        }
                    }
                    else
                    {
                        identifier = "";
                        state = State.Undefined;
                    }
                }
            }
            return identifiers;
        }

        static void PrintArray(string[] array)
        {
            if (array.Length == 0)
            {
                Console.WriteLine("Null");
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    Console.WriteLine($"[{i}] " + array[i]);
                }
            }
        }
    
        // Standart mode
        static Options ParseOptions(string[] args)
        {
            var options = new Options()
            {
                isInteractiveMode = false,
                inputFile = "",
                outputFile = "",
                parsingError = "",
            };

            bool[] isParsedArr = new bool[args.Length];

            while (true)
            {
                if (!ContainsValue(isParsedArr, false))
                {
                    break;
                }

                for (int i = 0; i < args.Length; i++)
                {
                    if (isParsedArr[i] == true)
                    {
                        continue;
                    }

                    string arg = args[i];
                    if (arg == "inputFile.txt")
                    {
                        options.inputFile = File.ReadAllText("./inputFile.txt");
                        isParsedArr[i] = true;
                    }
                    else if (arg == "-i")
                    {
                        options.isInteractiveMode = true;
                        isParsedArr[i] = true;
                    }
                    else if (arg == "-o")
                    {
                        if (i == args.Length - 1)
                        {
                            options.parsingError = "No value after '-o'";
                            return options;
                        }
                        string nextArg = args[i + 1];
                        if (nextArg.StartsWith('-'))
                        {
                            options.parsingError = "No value after '-o'";
                            return options;
                        }
                        options.outputFile = nextArg;
                        isParsedArr[i] = true;
                        isParsedArr[i + 1] = true;
                        
                    }
                    else if (arg.StartsWith('-'))
                    {
                        options.parsingError = $"Unknown option: '{arg}'";
                        return options;
                    }
                    else
                    {
                        if (options.inputFile != "")
                        {
                            options.parsingError = $"Input file was already set.";
                            return options;
                        }
                        options.inputFile = arg;
                        isParsedArr[i] = true;
                    }
                }
            }
            return options;
        }

        static bool ContainsValue(bool[] arr, bool value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == value)
                {
                    return true;
                }
            }
            return false;
        }

        static bool CompareOptions(in Options o1, in Options o2)
        {
        return o1.isInteractiveMode == o2.isInteractiveMode
            && o1.inputFile == o2.inputFile
            && o1.outputFile == o2.outputFile
            && o1.parsingError == o2.parsingError;
        }

    }
}
