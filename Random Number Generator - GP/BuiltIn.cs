using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_05_Random_Numbers_Generator
{
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
