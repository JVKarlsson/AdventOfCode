using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    class Day07
    {
        private readonly string _path;
        public Day07(string path)
        {
            _path = Path.Combine(path, "InputDay07.txt");

            PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var input = File.ReadAllLines(_path).ToList();

            HashSet<string> bags = new();
            GetBagsContaining(bags, input, "shiny gold");
            Console.WriteLine($"Advent of Code Day 07 part 1 : {bags.Count}");
        }

        private void GetBagsContaining(HashSet<string> bags, List<string> input, string toMatch)
        {
            var matched = input.Where(x => x.Split("contain")[1].Contains(toMatch)).ToList();
            matched.ForEach(x => input.Remove(x));
            var colors = matched.Select(x => GetBagColor(x)).ToList();
            colors.ForEach(x => { bags.Add(x); GetBagsContaining(bags, input, x); });
        }

        private string GetBagColor(string line)
        {
            var color = line.Split("contain").First().Split(" ").Take(2).Aggregate((prev, next) => prev + " " + next);
            return color;
        }



        public void PartTwo()
        {
            var input = File.ReadAllLines(_path).ToList();
            var sum = GetContainerBag(input, "shiny gold") - 1;
            Console.WriteLine($"Advent of Code Day 07 part 2 : {sum}");
        }

        private int GetContainerBag(List<string> input, string toMatch)
        {
            var sum = 1;

            var matched = input.Where(x => x.Substring(0, toMatch.Length) == toMatch).FirstOrDefault();
            if (matched is not null)
            {
                var content = matched.Split("contain").Skip(1).First()
                                    .Split(",")
                                    .Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries).Take(3).ToList())
                                    .Where(x => x.First() != "no")
                                    .Select(x => (int.Parse(x[0]), $"{x[1]} {x[2]}"));

                sum += content.Select(x => x.Item1 * GetContainerBag(input, x.Item2)).Sum();
            }
            return sum;
        }
    }
}
