using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;


namespace Project_05_Random_Numbers_Generator
{
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
}
