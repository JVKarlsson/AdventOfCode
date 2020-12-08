using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    class Day08
    {
        private readonly string _path;
        public Day08(string path)
        {
            _path = Path.Combine(path, "InputDay08.txt");

            PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var input = File.ReadAllLines(_path).ToList();
            _ = RunProgram(input, out int acc);
            Console.WriteLine($"Advent of Code Day 08 part 1 : {acc}");
        }

        public void PartTwo()
        {
            var input = File.ReadAllLines(_path).ToList();
            var nopAndJmp = input.Select((x, i) => (i, x)).Where(x => x.x[0] == 'n' || x.x[0] == 'j');

            int acc = 0;
            foreach (var line in nopAndJmp)
            {
                var newList = input.Select(x => x).ToList();
                newList[line.i] = line.x[0] == 'n' ? $"jmp {line.x.Split(" ")[1]}" : $"nop {line.x.Split(" ")[1]}";
                if (RunProgram(newList, out acc))
                    break;
            }

            Console.WriteLine($"Advent of Code Day 08 part 2 : {acc}");
        }

        private bool RunProgram(List<string> input, out int acc)
        {
            acc = 0;
            Dictionary<int, string> usedCommands = new();
            for (int i = 0; i < input.Count;)
            {
                var command = input[i];
                if (!usedCommands.TryAdd(i, input[i]))
                    return false;

                acc += command[0] == 'a' ? int.Parse(command.Split(" ")[1]) : 0;
                i += command[0] == 'j' ? int.Parse(command.Split(" ")[1]) : 1;

            }
            return true;
        }
    }
}
