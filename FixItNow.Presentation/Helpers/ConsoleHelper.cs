using System;

namespace FixItNow.Presentation.Helpers
{
    public static class ConsoleHelper
    {
        public static void PrintHeader(string title)
        {
            Console.Clear();
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"  {title}");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine();
        }

        public static int ReadInt(string prompt)
        {
            Console.Write(prompt);
            return int.Parse(Console.ReadLine() ?? "0");
        }

        public static string ReadString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? string.Empty;
        }

        public static void PressAnyKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}