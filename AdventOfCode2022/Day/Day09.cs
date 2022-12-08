using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day
{
    internal class Day09
    {
        private string _path;
        public Day09(string path)
        {
            _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
            PartOne();
            PartTwo();
        }
        public void PartOne()
        {
            var lines = File.ReadAllLines(_path);
            Console.WriteLine($"Advent of Code Day 09 part 1 : {0}");
        }

        public void PartTwo()
        {
            var lines = File.ReadAllLines(_path);
            Console.WriteLine($"Advent of Code Day 09 part 2 : {0}");
        }
    }
}
