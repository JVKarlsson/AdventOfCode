using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    class Day16
    {
        private readonly string _path;
        public Day16(string path)
        {
            _path = Path.Combine(path, "InputDay16.txt");

            PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var input = File.ReadAllLines(_path).ToList();
            Console.WriteLine($"Advent of Code Day 16 part 1 : {0}");
        }

        public void PartTwo()
        {
            var input = File.ReadAllLines(_path).ToList();
            Console.WriteLine($"Advent of Code Day 16 part 2 : {0}");
        }
    }
}
