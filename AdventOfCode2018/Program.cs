using AdventOfCode2018.Day;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Program
    {
        static void Main(string[] args)
        {
            Day1 day = new Day1();
            int frequenzy = day.GetResultingFrequenzy();
            Console.WriteLine(frequenzy);

            Console.ReadLine();
        }
    }
}
