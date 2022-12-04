using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day
{
    internal class Day05
    {
        private string _path;
        public Day05(string path)
        {
            _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
            PartOne();
            PartTwo();
        }
        public void PartOne()
        {
            var result = File.ReadAllLines(_path);
            Console.WriteLine($"Advent of Code Day 05 part 1 : {result}");
        }

        public void PartTwo()
        {
            var result = File.ReadAllLines(_path);
            Console.WriteLine($"Advent of Code Day 05 part 2 : {result}");
        }
    }
}
