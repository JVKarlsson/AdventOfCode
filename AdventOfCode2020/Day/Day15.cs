using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    class Day15
    {
        private string _path;
        public Day15(string path)
        {
            _path = Path.Combine(path, "InputDay15.txt");

            //PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var input = File.ReadAllLines(_path).Select(x => x.Split(",").Select(y => int.Parse(y)).ToList()).First();

            List<(int,int)> numbers = input.Select((x, i) => (index: i, value: x)).ToList();
            while(numbers.Count != 2020)
            {
                var latest = numbers.Where(x => x.Item2 == numbers.Last().Item2).ToList();

                var newNum = 0;
                if (latest.Count > 1)
                {
                    latest.Reverse();
                    var test = latest.Take(2);
                    newNum = latest[0].Item1 - latest[1].Item1;
                }

                numbers.Add((numbers.Count(), newNum));

            }
            var last = numbers.Last().Item2;
            Console.WriteLine($"Advent of Code Day 15 part 1 : {last}");
        }

        public void PartTwo()
        {
            var input = File.ReadAllLines(_path).Select(x => x.Split(",").Select(y => int.Parse(y)).ToList()).First();
            Dictionary<int, (int,int)> numbers = new();
            for (int i = 0; i < input.Count(); i++)
            {
                numbers.Add(input[i], (0, i + 1));
            }
            //numbers.TryAdd(0, (input.Count() + 1, 0));

            int latest = input.Last();
            for (int i = input.Count+1; i <= 30000000; i++)
            {
                if (numbers.ContainsKey(latest))
                {
                    var indexes = numbers[latest];
                    if (indexes.Item1 > 0 && indexes.Item2 > 0)
                        latest = indexes.Item2 - indexes.Item1;
                    else
                        latest = 0;
                }
                else
                {
                    latest = 0;
                }



                if (numbers.ContainsKey(latest))
                {
                    var indexes = numbers[latest];
                    numbers[latest] = (indexes.Item2, i);
                }
                else
                {
                    numbers.Add(latest, (0, i));
                }
            }
            Console.WriteLine($"Advent of Code Day 15 part 2 : {latest}");
        }
    }
}
