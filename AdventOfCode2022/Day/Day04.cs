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
            var lines = File.ReadAllLines(_path)
                .Select(x => x.Split(','))
                .ToList();

            int result = 0;
            foreach (var item in lines)
            {
                var firstNum = item[0].Split('-').Select(x => int.Parse(x)).ToList();
                var first = Enumerable.Range(firstNum[0], firstNum[1] - firstNum[0] + 1).ToList();
                

                var secondNum = item[1].Split('-').Select(x => int.Parse(x)).ToList();
                var second = Enumerable.Range(secondNum[0], secondNum[1] - secondNum[0] + 1).ToList();

                var intersects = first.Intersect(second).ToList();

                if (intersects.Count == first.Count || intersects.Count == second.Count)
                {
                    result++;
                }
            }

            Console.WriteLine($"Advent of Code Day 04 part 1 : {result}");
        }

        public void PartTwo()
        {
            var lines = File.ReadAllLines(_path)
                .Select(x => x.Split(','))
                .ToList();

            int result = 0;
            foreach (var item in lines)
            {
                var firstNum = item[0].Split('-').Select(x => int.Parse(x)).ToList();
                var first = Enumerable.Range(firstNum[0], firstNum[1] - firstNum[0] + 1).ToList();


                var secondNum = item[1].Split('-').Select(x => int.Parse(x)).ToList();
                var second = Enumerable.Range(secondNum[0], secondNum[1] - secondNum[0] + 1).ToList();

                var intersects = first.Intersect(second).ToList();

                if (intersects.Count > 0)
                {
                    result++;
                }
            }

            Console.WriteLine($"Advent of Code Day 04 part 2 : {result}");
        }
    }
}
