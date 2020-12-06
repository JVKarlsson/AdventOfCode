using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    class Day03
    {
        private string _path;
        public Day03(string path)
        {
            _path = Path.Combine(path, "InputDay03.txt"); ;
            PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var input = File.ReadAllLines(_path).ToList();
            Console.WriteLine($"Advent of Code Day 03 part 1 : {GetTrees(input, 3, 1)}");
        }

        public void PartTwo()
        {
            var input = File.ReadAllLines(_path).ToList();

            var movements = new[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };
            var result = movements.Select(x => GetTrees(input, x.Item1, x.Item2)).Aggregate(1L, (x, y) => x * y);

            Console.WriteLine($"Advent of Code Day 03 part 2 : {result}");
        }

        private int GetTrees(List<string> input, int right, int down)
        {
            var count = 0;
            var xPos = 0;
            for (int yPos = 0; yPos < input.Count; yPos += down)
            {
                if (input[yPos].ElementAt(xPos) == '#')
                    count++;
                xPos = (xPos + right) % input[yPos].Length;
            }
            return count;
        }
    }
}
