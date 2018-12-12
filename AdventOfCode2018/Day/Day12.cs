using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day
{
    class Day12
    {
        private static string path = "";
        public Day12(string p)
        {
            path = Path.Combine(p, "InputDay12.txt");
            Console.WriteLine("Advent of Code Day 7 part 1 : " + PartOne());
        }

        private string PartOne()
        {
            var lines = File.ReadAllLines(path).ToList();
            var instructions = lines.GetRange(2, lines.Count - 2);
            var initial = lines.First().Substring(15);

            for (int i = 0; i < 20; i++)
            {
                foreach (var instruction in instructions)
                {
                    var split = instruction.Split(new char[] { '=', '>' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

                    var copy = initial;
                    var indexes = new List<int>();
                    
                    while (true)
                    {
                        var c = copy.Count();
                        var hit = copy.IndexOf(split[0]);
                        copy = copy.Substring(hit + 1);
                        if (hit < 0)
                            break;
                        indexes.Add(hit);
                    }

                }
            }

            return "#";
        }
    }
}
