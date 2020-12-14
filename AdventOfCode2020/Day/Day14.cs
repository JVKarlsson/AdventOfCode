using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    class Day14
    {
        private string _path;
        public Day14(string path)
        {
            _path = Path.Combine(path, "InputDay14.txt");

            //PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var input = File.ReadAllLines(_path).Select(x => x.Split("=").Select(x => x.Trim()).ToArray());
            Dictionary<string, long> memory = new();

            input.Where(x => x[0].Substring(0, 3) == "mem").ToList().ForEach(x => { memory.TryAdd(x[0], 0); });
            List<(char, int)> mask = new();

            foreach (var line in input)
            {
                if (line[0] == "mask")
                {
                    mask = line[1].Select((value, index) => (value, index)).Where(x => x.value != 'X').ToList();
                }
                else
                {
                    var val = Convert.ToString(long.Parse(line[1]), 2).PadLeft(36, '0');
                    mask.ForEach(x =>
                    {
                        var copy = val.ToCharArray();
                        copy[x.Item2] = x.Item1;
                        val = new string(copy);
                    });

                    var newVal = Convert.ToInt64(val, 2);
                    memory[line[0]] = newVal;
                }
            }

            var sum = memory.Values.Sum();
            Console.WriteLine($"Advent of Code Day 14 part 1 : {sum}");
        }

        public void PartTwo()
        {
            var input = File.ReadAllLines(_path).Select(x => x.Split("=").Select(x => x.Trim()).ToArray()).ToList();
            Dictionary<string, long> memory = new();

            WriteResultToMemory("000000000000000000000000000000X1101X", memory);

            foreach (var line in input)
            {

            }

            Console.WriteLine($"Advent of Code Day 14 part 2 : {0}");
        }

        private void WriteResultToMemory(string value, Dictionary<string, long> memory)
        {
            var occurances = value.Select((x, i) => (index: i, value: x)).Where(x => x.value == 'X').ToList();
            var numOfentries = Math.Pow(2, value.Where(x => x == 'X').Count());
        }
    }
}
