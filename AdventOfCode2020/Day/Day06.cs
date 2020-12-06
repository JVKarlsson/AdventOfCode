using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    class Day06
    {
        private string _path;
        public Day06(string path)
        {
            _path = Path.Combine(path, "InputDay06.txt");
            PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var sum = File.ReadAllText(_path)
                            .Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => x.Replace(Environment.NewLine, ""))
                            .Select(x => new HashSet<char>(x))
                            .Aggregate(0, (x, y) => x + y.Count);

            Console.WriteLine($"Advent of Code Day 06 part 1 : {sum}");
        }

        public void PartTwo()
        {
            var sum = File.ReadAllText(_path)
                            .Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => x.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                            .Select(x => x.Aggregate((x, y) => new string(x.Intersect(y).ToArray())))
                            .Aggregate(0, (x,y) => x+y.Length);
            Console.WriteLine($"Advent of Code Day 06 part 2 : {sum}");
        }
    }
}
