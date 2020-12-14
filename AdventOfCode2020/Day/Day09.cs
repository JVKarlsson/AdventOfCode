using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    class Day09
    {
        private readonly string _path;
        public Day09(string path)
        {
            _path = Path.Combine(path, "InputDay09.txt");

            PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var input = File.ReadAllLines(_path).Select(x => double.Parse(x)).ToList();

            //var test = input.Select((x, i) => HasSum(i, input)).ToList();

            double result = 0;
            for (int i = 0; i < input.Count; i++)
            {
                if (!HasSum(i, input))
                {
                    result = input[i + 25];
                    break;
                }
            }

            Console.WriteLine($"Advent of Code Day 09 part 1 : {result}");
        }

        public void PartTwo()
        {
            var input = File.ReadAllLines(_path).Select(x => double.Parse(x)).ToList();

            //var test = input.Select((x, i) => HasSum(i, input)).ToList();

            double invalidnumber = 0;
            for (int i = 0; i < input.Count; i++)
            {
                if (!HasSum(i, input))
                {
                    invalidnumber = input[i + 25];
                    break;
                }
            }

            double result = 0;
            for (int i = 0; i < input.Count; i++)
            {
                List<double> resultSet = new() { input[i] };
                double currentSum = input[i];
                var success = false;
                for (int j = i + 1; j < input.Count - 1; j++)
                {
                    currentSum += input[j];
                    if (currentSum < invalidnumber)
                        resultSet.Add(input[j]);
                    else if (currentSum == invalidnumber && resultSet.Count > 0)
                    {
                        success = true;
                        resultSet.Add(input[j]);
                        break;
                    }
                }
                if (success)
                {
                    result = resultSet.Min() + resultSet.Max();
                    break;
                }
            }
            Console.WriteLine($"Advent of Code Day 09 part 2 : {result}");
        }

        private bool HasSum(int offset, List<double> input)
        {
            var sum = input.Skip(25 + offset).FirstOrDefault();
            var options = input.Skip(offset).Take(25).Distinct().ToList();

            foreach (var number in options)
            {
                var result = options.Where(x => x != number).Any(y => y + number == sum);
                if (result)
                    return true;
            }
            return false;
        }
    }
}
