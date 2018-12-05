using AdventOfCode2018.Day;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetFullPath(@"..\..\"), "Inputs");

            //Day1 day1 = new Day1(path);
            //Day2 day2 = new Day2(path);
            //Day3 day3 = new Day3(path);
            //Day4 day4 = new Day4(path);
            Day5 day5 = new Day5();
            Console.ReadLine();
        }
    }
}
