using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day
{
    internal class Day01
    {
        private string _path;
        public Day01(string path)
        {
            _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
            PartOne();
            PartTwo();
        }
        public void PartOne()
        {
            var result = File.ReadAllText(_path)
                .Split("\r\n\r\n")
                .Select(x => x.Split("\r\n").Sum(y => int.Parse(y)))
                .Max();

            Console.WriteLine($"Advent of Code Day 01 part 1 : {result}");
        }

        public void PartTwo()
        {
            var result = File.ReadAllText(_path)
                .Split("\r\n\r\n")
                .Select(x => x.Split("\r\n").Sum(y => int.Parse(y)))
                .OrderByDescending(x => x)
                .Take(3)
                .Sum();

            Console.WriteLine($"Advent of Code Day 01 part 2 : {result}");
        }
    }
}
