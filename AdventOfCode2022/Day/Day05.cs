using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day
{
    internal class Day05
    {
        private string _path;
        public Day05(string path)
        {
            _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
            PartOne();
            PartTwo();
        }
        public void PartOne()
        {
            var lines = File.ReadAllLines(_path);
            List<Stack<char>> stacks = new();

            var seperatorLine = lines.ToList().IndexOf("");
            var crates = lines[..(seperatorLine - 1)].Select((x) => x.Replace("[", "").Replace("]", "").Replace("    ", ".").Replace(" ", ""));;
            var length = crates.Max(x => x.Length);

            for (int i = 0; i < length; i++)
            {
                var stack = new Stack<char>();
                crates.Reverse().ToList().ForEach(x =>
                {
                    var item = x.ElementAtOrDefault(i);
                    if (item is not '\0' && item is not '.')
                    {
                        stack.Push(item);
                    }
                });
                stacks.Add(stack);
            }


            var inputs = lines[(seperatorLine + 1)..];
            foreach (var input in inputs)
            {
                var test = input
                    .Replace("move ", "")
                    .Replace(" from ", ",")
                    .Replace(" to ", ",");
                var numbers = test.Split(',').Select(x => int.Parse(x)).ToArray();

                for (int i = 0; i < numbers[0]; i++)
                {
                    var character = stacks[numbers[1]-1].Pop();
                    stacks[numbers[2]-1].Push(character);
                }
            }
            var result = String.Concat(stacks.Select(x => x.Pop()));
            Console.WriteLine($"Advent of Code Day 05 part 1 : {result}");
        }

        public void PartTwo()
        {
            var lines = File.ReadAllLines(_path);
            List<Stack<char>> stacks = new();

            var seperatorLine = lines.ToList().IndexOf("");
            var crates = lines[..(seperatorLine - 1)].Select((x) => x.Replace("[", "").Replace("]", "").Replace("    ", ".").Replace(" ", "")); ;
            var length = crates.Max(x => x.Length);

            for (int i = 0; i < length; i++)
            {
                var stack = new Stack<char>();
                crates.Reverse().ToList().ForEach(x =>
                {
                    var item = x.ElementAtOrDefault(i);
                    if (item is not '\0' && item is not '.')
                    {
                        stack.Push(item);
                    }
                });
                stacks.Add(stack);
            }


            var inputs = lines[(seperatorLine + 1)..];
            foreach (var input in inputs)
            {
                var test = input
                    .Replace("move ", "")
                    .Replace(" from ", ",")
                    .Replace(" to ", ",");
                var numbers = test.Split(',').Select(x => int.Parse(x)).ToArray();

                var list = new List<char>();
                for (int i = 0; i < numbers[0]; i++)
                {
                    var character = stacks[numbers[1] - 1].Pop();
                    list.Add(character);
                }
                list.Reverse();
                list.ForEach(x => stacks[numbers[2] - 1].Push(x));
            }
            var result = String.Concat(stacks.Select(x => x.Pop()));
            Console.WriteLine($"Advent of Code Day 05 part 2 : {result}");
        }
    }
}
