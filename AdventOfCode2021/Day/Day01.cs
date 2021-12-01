using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2021.Day
{
    public class Day01
    {
        private readonly string _path;
        public Day01(string path)
        {
            _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
            PartOne();
            PartTwo();
        }
        private void PartOne()
        {
            int count = 0;
            var input = File.ReadAllLines(_path)
                .Aggregate((prev, next) =>  { count += int.Parse(prev) < int.Parse(next) ? 1 : 0; return next; });
            Console.WriteLine($"Advent of Code Day 01 part 1 : {count}");
        }

        private void PartTwo()
        {
            int count = 0;
            var input = File.ReadAllLines(_path).Select(x => int.Parse(x)).ToArray();
            var asd = input[..(input.Length - 2)]
                .Select((x, i) => input[i..(i + 3)].Sum())
                .Aggregate((prev, next) => { count += prev < next ? 1 : 0; return next; });

            Console.WriteLine($"Advent of Code Day 01 part 2 : {count}");
        }
    }
}
