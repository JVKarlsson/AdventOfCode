using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    class Day10
    {
        private readonly string _path;


        public Day10(string path)
        {
            _path = Path.Combine(path, "InputDay10.txt");

            PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var input = File.ReadAllLines(_path).Select(x => int.Parse(x)).Distinct().OrderBy(x => x).ToList();

            var DeviceJoltage = input.Last() + 3;
            var currentJoltage = 0;
            List<int> differences = new();

            while (input.Count > 0)
            {
                var valid = input.Where(x => x - 3 <= currentJoltage).ToList();
                differences.Add(valid.First() - currentJoltage);
                currentJoltage = valid.First();

                input = input.Where(x => x > currentJoltage).ToList();
            }
            differences.Add(3);
            var jolt1 = differences.Count(x => x == 1);
            var jolt3 = differences.Count(x => x == 3);
            var sum = jolt1 * jolt3;
            Console.WriteLine($"Advent of Code Day 10 part 1 : {sum}");
        }



        public void PartTwo()
        {
            var input = File.ReadAllLines(_path).Select(x => int.Parse(x)).Distinct().OrderBy(x => x).ToList();

            input.Add(input.Last() + 3);
            input.Insert(0,0);

            Dictionary<long, long> results = new ();
            var sum = ArrangeAdapters(input.ToArray(), results);

            Console.WriteLine($"Advent of Code Day 10 part 2 : {sum}");
        }

        private long ArrangeAdapters(int[] input, Dictionary<long, long> results)
        {
            if (results.ContainsKey(input.Length))
            {
                return results[input.Length];
            }

            if (input.Length == 1)
            {
                return 1;
            }

            long count = 0;
            long currentJoltage = input[0];

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] - 3 <= currentJoltage)
                {
                    count += ArrangeAdapters(input[i..], results);
                }
                else
                {
                    break;
                }
            }

            results.Add(input.Length, count);

            return count;
        }

        private long CountAdapters(List<int> input, int currentJoltage)
        {
            var valid = input.Where(x => x - 3 <= currentJoltage).ToList();
            long count = valid.Count == 0 ? 1 : 0;
            count += valid.Select(x => CountAdapters(input.Where(y => y > x).ToList(), x)).Sum();
            return count;
        }
    }
}