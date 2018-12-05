using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Day
{
    class Day5
    {

        public Day5()
        {
            var input = File.ReadAllText("InputDay5.txt");
            Console.WriteLine("Advent of Code Day 5 part 1 : Polymer length " + PartOne());
            Console.WriteLine("Advent of Code Day 5 part 2 : Shortest polymer length " + PartTwo());
        }

        public int PartOne()
        {
            List<char> lines = File.ReadAllText("InputDay5.txt").ToList();
            return GetPolymerCountFromList(lines);
        }

        public int PartTwo()
        {
            var occurance = File.ReadAllText("InputDay5.txt").ToUpper().Distinct().ToList();

            var listcounter = new List<int>();
            foreach (var o in occurance)
            {
                var lines = File.ReadAllText("InputDay5.txt").Replace(char.ToLower(o).ToString(), "").Replace(char.ToUpper(o).ToString(), "").ToList();
                int count = GetPolymerCountFromList(lines);
                listcounter.Add(count);
            }
            return listcounter.OrderBy(x => x).First();
        }

        private int GetPolymerCountFromList(List<char> lines)
        {
            for (int i = 0; i < (lines.Count - 1);)
            {
                if (!(lines[i] == lines[i + 1]) && (char.ToLower(lines[i]) == char.ToLower(lines[i + 1])))
                {
                    lines.RemoveAt(i + 1);
                    lines.RemoveAt(i);
                    if (i != 0)
                        i--;
                }
                else
                    i++;
            }
            return lines.Count();
        }
    }
}
