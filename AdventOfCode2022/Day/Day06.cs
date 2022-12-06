using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day
{
    internal class Day06
    {
        private string _path;
        public Day06(string path)
        {
            _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
            PartOne();
            PartTwo();
        }
        public void PartOne()
        {
            var result = Solve(4);
            Console.WriteLine($"Advent of Code Day 06 part 1 : {result}");
        }

        public void PartTwo()
        {
            var result = Solve(14);
            Console.WriteLine($"Advent of Code Day 06 part 2 : {result}");
        }

        private int Solve(int distinctCharacters)
        {
            var lines = File.ReadAllText(_path);
            var result = distinctCharacters;
            for (int i = 0; i < lines.Length; i++)
            {
                var count = lines[(i)..(i + distinctCharacters)].Distinct().Count();
                if (count == distinctCharacters)
                {
                    result += i;
                    break;
                }
            }
            return result;
        }
    }
}
