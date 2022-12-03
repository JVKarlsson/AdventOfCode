using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day
{
    internal class Day03
    {
        private string _path;
        public Day03(string path)
        {
            _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
            PartOne();
            PartTwo();
        }
        public void PartOne()
        {
            var test = File.ReadAllLines(_path);
            var test2 = test.Partition(test.Length / 2).ToList();
            var first = test2[0];
            var second = test2[1];

            var result = File.ReadAllLines(_path)
                .Select( x =>
                {
                    // If multiple values can be found, replace first with select.
                    var first = x[..(x.Length / 2)];
                    var second = x[(x.Length / 2)..];
                    var intersects = first.Intersect(second).First();
                    return Char.IsUpper(intersects) ? intersects - (64 - 26) : intersects - 96;
                })
                .Sum();
            Console.WriteLine($"Advent of Code Day 03 part 1 : {result}");
        }

        public void PartTwo()
        {
            var result = File.ReadAllLines(_path)
                .Partition(3)
                .Select(x =>
                {
                    // If multiple values can be found, replace first with select.
                    var intersects = x[0].Intersect(x[1]).Intersect(x[2]).ToArray().First();
                    return Char.IsUpper(intersects) ? intersects - (64 - 26) : intersects - 96;
                })
                .Sum();
            Console.WriteLine($"Advent of Code Day 03 part 2 : {result}");
        }
    }
}
