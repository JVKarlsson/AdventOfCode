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
            var result = File.ReadAllLines(_path)
                .Select( x =>
                {
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
                .Chunk(3)
                .Select(x =>
                {
                    var intersects = x[0].Intersect(x[1]).Intersect(x[2]).First();
                    return Char.IsUpper(intersects) ? intersects - (64 - 26) : intersects - 96;
                })
                .Sum();
            Console.WriteLine($"Advent of Code Day 03 part 2 : {result}");
        }
    }
}
