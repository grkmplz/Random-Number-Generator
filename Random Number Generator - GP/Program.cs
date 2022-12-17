using System;

namespace Project_05_Random_Numbers_Generator
{
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
}
