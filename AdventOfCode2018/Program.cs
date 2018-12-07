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

            //var day1 = new Day1(path);
            //var day2 = new Day2(path);
            //var day3 = new Day3(path);
            //var day4 = new Day4(path);
            //var day5 = new Day5(path);
            //var day6 = new Day6(path);
            var Day7 = new Day7(path);

            Console.ReadLine();
        }
    }
}
