using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Day
{
    class Day2
    {
        private string _path = "";
        public Day2(string path)
        {
            _path = Path.Combine(path, "InputDay2.txt");
            Console.WriteLine("Advent of Code Day 2 part 1 : Value left = " + PartOne());
            Console.WriteLine("Advent of Code Day 2 part 2 : Value left = " + PartTwo());
        }

        private int PartOne()
        {
            var codeList = File.ReadAllText(_path).Split(",").Select(x => int.Parse(x)).ToList();
            codeList[1] = 12;
            codeList[2] = 2;
            return RunCode(codeList.ToList());
        }

        private int PartTwo()
        {
            var originalList = File.ReadAllText(_path).Split(",").Select(x => int.Parse(x)).ToList();
            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    var codeList = originalList.ToList();
                    codeList[1] = noun;
                    codeList[2] = verb;
                    if (19690720 == RunCode(codeList))
                    {
                        return 100 * noun + verb;
                    }
                }
            }
            return -1;
        }
        private int RunCode(List<int> codeList)
        {
            for (int i = 0; i < codeList.Count; i += 4)
            {
                switch (codeList[i])
                {
                    case 1:
                        codeList[codeList[i + 3]] = codeList[codeList[i + 1]] + codeList[codeList[i + 2]];
                        break;
                    case 2:
                        codeList[codeList[i + 3]] = codeList[codeList[i + 1]] * codeList[codeList[i + 2]];
                        break;
                    case 99:
                        return codeList[0];
                    default:
                        break;
                }
            }
            return codeList[0];
        }
    }
}
