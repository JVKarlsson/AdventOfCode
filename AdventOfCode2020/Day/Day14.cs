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

            PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var input = File.ReadAllLines(_path).Select(x => x.Split("=").Select(x => x.Trim()).ToArray());
            Dictionary<string, long> memory = new();
            List<(char, int)> mask = new();

            input.Where(x => x[0].Substring(0, 3) == "mem").ToList().ForEach(x => { memory.TryAdd(x[0], 0); });
            foreach (var line in input)
            {
                if (line[0] == "mask")
                {
                    mask = line[1].Select((value, index) => (value, index)).Where(x => x.value != 'X').ToList();
                }
                else
                {
                    var val = ApplyMask(line[1], mask);
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
            List<(char, int)> mask = new();

            foreach (var line in input)
            {
                if (line[0] == "mask")
                {
                    mask = line[1].Select((value, index) => (value, index)).Where(x => x.value != '0').ToList();
                }
                else
                {
                    var val = line[0].Split("[")[1].Split("]")[0];
                    val = ApplyMask(val, mask);
                    WriteResultToMemory(val, memory, long.Parse(line[1]));
                }
            }

            var sum = memory.Values.Sum();
            Console.WriteLine($"Advent of Code Day 14 part 2 : {sum}");
        }

        private string ApplyMask(string value, List<(char, int)> mask)
        {
            var val = Convert.ToString(long.Parse(value), 2).PadLeft(36, '0');
            mask.ForEach(x =>
            {
                var copy = val.ToCharArray();
                copy[x.Item2] = x.Item1;
                val = new string(copy);
            });
            return val;
        }

        private void WriteResultToMemory(string value, Dictionary<string, long> memory, long toWrite)
        {
            var occurances = value.Select((x, i) => (index: i, value: x)).Where(x => x.value == 'X').ToList();
            var numOfentries = Math.Pow(2, value.Where(x => x == 'X').Count());

            for (int i = 0; i < numOfentries; i++)
            {
                var copy = value.ToCharArray();
                var val = Convert.ToString(i, 2).PadLeft(occurances.Count, '0');

                for (int j = 0; j < val.Length; j++)
                {
                    copy[occurances[j].index] = val[j];
                }
                val = new string(copy);
                if (memory.ContainsKey(val))
                    memory[val] = toWrite;
                else
                    memory.Add(val, toWrite);

            }

        }
    }
}
