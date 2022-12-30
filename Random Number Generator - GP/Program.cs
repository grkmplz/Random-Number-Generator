using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace Project_05_Random_Numbers_Generator
{
    class Console_ML
    {
        public string Labelx { get; set; }
        public Action RunAction { get; set; }
        public Console_ML(string label, Action runaction)
        {
            Labelx = label;
            RunAction = runaction;
        }
        public override string ToString()
        {
            return Labelx;
        }
    }
    class ConMenuClass
    {
        private bool notExit = true;
        private string Description { get; set; }
        private Console_ML[] MenuElements { get; set; }
        public int SelectedItem { get; private set; }
        public ConMenuClass(string desc, Console_ML[] menu_List)
        {
            SelectedItem = 0;
            Description = desc;
            MenuElements = menu_List;
        }
        public void MenuDisplay()
        {
            Console.WriteLine(Description);
            for (int i = 0; i < MenuElements.Length; i++)
            {
                if (i == SelectedItem)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(MenuElements[i]);
                Console.ResetColor();
            }
        }
        public void RunMenu()
        {
            while (notExit)
            {
                Console.Clear();
                MenuDisplay();
                ConsoleKeyInfo pressedKey = Console.ReadKey();
                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        SelectedItem = (SelectedItem - 1 + MenuElements.Length) % MenuElements.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        SelectedItem = (SelectedItem + 1) % MenuElements.Length;
                        break;
                    case ConsoleKey.Enter:
                        if (MenuElements.Length == 0) { break; }
                        MenuElements[SelectedItem].RunAction();
                        break;
                    case ConsoleKey.Escape:
                        notExit = false;
                        break;
                }
            }
        }
    }
    interface IRNG
    {
        public long Next();
        public long Next(int Valmax);
        public long Next(int Valmin, int Valmax);
        public void Display(int Valmin, int Valmax, int n);
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console_ML[] menu_List = {
                new Console_ML("C# Built-in", RunBuiltIn),
                new Console_ML("LCG", RunLCG),
                new Console_ML("Xorshift", RunXorshift)
            };
            ConMenuClass conMenu = new ConMenuClass("Continue with RNG ..", menu_List);
            conMenu.RunMenu();
        }

        static int[] GetRange()
        {
            Console.Write("Interval (Miimum Value - maximum Value, Don't forget to use spaces between '-'): ");
            string getinput = Console.ReadLine();
            string[] Arraynput = RangeCheck(getinput);
            if(Arraynput.Length == 1)
            {
                return new int[1];
            }
            int[] interval = new int[2] { int.Parse(Arraynput[0].Replace(" ", "")), int.Parse(Arraynput[1].Replace(" ", "")) };

            return interval;
        }
        static string[] RangeCheck(string getinput)
        {
            if (getinput.Split(" - ").Length != 2 || string.IsNullOrWhiteSpace(getinput))
            {
                Console.WriteLine("Incorrect input!! Examples of correct input:\n 0 - 10\n-4 - 2\n7 - 14");
                return new string[1];
            }

            foreach (var c in getinput.Replace("-", "").Replace(" ", ""))
            {
                if (!char.IsDigit(c))
                {
                    Console.WriteLine("Only numbers are allowed!! Examples of correct input:\n 0 - 10\n-4 - 2\n7 - 14");
                    return new string[1];
                }
            }
            if(int.Parse(getinput.Split(" - ")[0].Replace(" ", "")) >= int.Parse(getinput.Split(" - ")[1].Replace(" ", "")))
            {
                Console.WriteLine("Max Value must be greater than Minimum Value!!");
                return new string[1];
            }
            return getinput.Split(" - ");
        }
        static void RunBuiltIn()
        {
            IRNG randomNG = new Built_in();
            var interval = GetRange();
            if(interval.Length != 1)
            {
                randomNG.Display(interval[0], interval[1], 100000);
            }
            Console.ReadLine();
        }
        static void RunXorshift()
        {
            IRNG randomNG = new Xorshift();
            var interval = GetRange();
            if (interval.Length != 1)
            {
                randomNG.Display(interval[0], interval[1], 100000);
            }
            Console.ReadLine();
        }
        static void RunLCG()
        {
            IRNG randomNG = new LCG();
            var interval = GetRange();
            if (interval.Length != 1)
            {
                randomNG.Display(interval[0], interval[1], 100000);
            }
            Console.ReadLine();
        }
    }
    class LCG : IRNG
    {
        private const long increment = 1013904223;
        private const long multiplier = 1664525;
        private long seed;

        public LCG(long seed = 110103)
        {
            this.seed = seed;
        }
        public long Next()
        {
            seed = ((multiplier * seed) + increment) % 4294967296;
            return seed;
        }

        public long Next(int Valmax)
        {
            return Next() % Valmax;
        }
        public long Next(int Valmin, int Valmax)
        {
            return Valmin + Next() % (Valmax - Valmin);
        }

        public void Display(int Valmin, int Valmax, int n)
        {
            Stopwatch sw = new Stopwatch();
            int[] counter = new int[Valmax - Valmin];
            sw.Restart();
            for (int i = 0; i < n; i++)
            {
                long number = Next(Valmin, Valmax);
                counter[number - Valmin]++;
            };
            sw.Stop();
            int id = 0;
            Console.WriteLine("Results: ");
            for (int i = Valmin; i < Valmax; i++)
            {
                double value = counter[id++] * 100.0 / n;
                Console.Write($"{i}: ");
                Console.Write(string.Join("", Enumerable.Repeat("*", Convert.ToInt32(Math.Floor(value)))));
                Console.Write($" {value}%");
                Console.WriteLine();
            }
        }
    }
    class Xorshift : IRNG
    {
        private ulong seed = 110103;
        public Xorshift(ulong seed = 110103)
        {
            this.seed = seed;
        }
        public long Next()
        {
            seed ^= seed << 13;
            seed ^= seed >> 17;
            seed ^= seed << 5;
            return (long)(seed % long.MaxValue);
        }
        public long Next(int Valmax)
        {
            return Next() % Valmax;
        }
        public long Next(int Valmin, int Valmax)
        {
            return Valmin + Next() % (Valmax - Valmin);
        }
        public void Display(int Valmin, int Valmax, int n)
        {
            Stopwatch sw = new Stopwatch();
            int[] counter = new int[Valmax - Valmin];
            sw.Restart();
            for (int i = 0; i < n; i++)
            {
                long number = Next(Valmin, Valmax);
                counter[number - Valmin]++;
            }
            sw.Stop();
            int id = 0;
            Console.WriteLine("Results: ");
            for (int i = Valmin; i < Valmax; i++)
            {
                double value = counter[id++] * 100.0 / n;
                Console.Write($"{i}: ");
                Console.Write(string.Join("", Enumerable.Repeat("*", Convert.ToInt32(Math.Floor(value)))));
                Console.Write($" {value}%");
                Console.WriteLine();
            }
        }
    }
    class Built_in : IRNG
    {
        Random randomNG = new Random();
        public long Next()
        {
            return randomNG.Next(int.MinValue, int.MaxValue);
        }
        public long Next(int Valmax)
        {
            return randomNG.Next(0, Valmax);
        }
        public long Next(int Valmin, int Valmax)
        {
            return randomNG.Next(Valmin, Valmax);
        }

        public void Display(int Valmin, int Valmax, int n)
        {
            Stopwatch sw = new Stopwatch();
            int[] counter = new int[Valmax - Valmin];
            sw.Restart();
            for (int i = 0; i < n; i++)
            {
                long number = Next(Valmin, Valmax);
                counter[number - Valmin]++;
            }
            sw.Stop();
            int id = 0;
            Console.WriteLine("Results: ");
            for (int i = Valmin; i < Valmax; i++)
            {
                double value = counter[id++] * 100.0 / n;
                Console.Write($"{i}: ");
                Console.Write(string.Join("", Enumerable.Repeat("*", Convert.ToInt32(Math.Floor(value)))));
                Console.Write($" {value}%");
                Console.WriteLine();
            }
        }
    }



}
