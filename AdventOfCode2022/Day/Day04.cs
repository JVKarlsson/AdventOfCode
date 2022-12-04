using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day
{
    internal class Day04
    {
        private string _path;
        public Day04(string path)
        {
            _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
            PartOne();
            PartTwo();
        }
        public void PartOne()
        {
            var result = File.ReadAllLines(_path)
                .Select(x => x.Split(','))
                .Select(x =>
                {
                    var first = GetRange(x[0]);
                    var second = GetRange(x[1]);
                    var intersects = first.Intersect(second).Count();
                    return (intersects == first.Count || intersects == second.Count) ? 1 : 0;
                })
                .Sum();
            Console.WriteLine($"Advent of Code Day 04 part 1 : {result}");
        }

        public void PartTwo()
        {
            var result = File.ReadAllLines(_path)
                .Select(x => x.Split(','))
                .Select(x =>
                {
                    var first = GetRange(x[0]);
                    var second = GetRange(x[1]);
                    var intersects = first.Intersect(second).Count();
                    return intersects > 0 ? 1 : 0;
                })
                .Sum();
            Console.WriteLine($"Advent of Code Day 04 part 2 : {result}");
        }

        private static List<int> GetRange(string x)
        {
            var numbers = x.Split('-').Select(x => int.Parse(x)).ToList();
            return Enumerable.Range(numbers[0], numbers[1] - numbers[0] + 1).ToList();
        }
    }
}
