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
            Console.WriteLine("Advent of Code Day 12 part 1 : " + PartOne());
        }

        private int PartOne()
        {
            var lines = File.ReadAllLines(path).ToList();
            var instructions = lines.GetRange(2, lines.Count - 2);
            var initial = lines.First().Substring(15);

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(initial);
                var copy = initial;
                foreach (var instruction in instructions)
                {
                    var split = instruction.Split(new char[] { '=', '>' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
                    var check = split[0];

                    var temp = copy.ToCharArray();
                    for (int j = 0; j < copy.Length; j++)
                    {
                        if (copy[j] == check[2])
                        {
                            var t = -2;
                            var isCorrect = true;
                            foreach (var pot in check)
                            {
                                var element = copy.ElementAtOrDefault(j - t).ToString();
                                element = string.IsNullOrEmpty(element) ? "." : element;

                                isCorrect = element == pot.ToString() ? true : false;
                                if (!isCorrect)
                                    break;
                                t++;
                            }
                            if (isCorrect)
                            {
                                temp[j] = split[1].ToCharArray().First();
                            }
                            // maybe check if the first one is changed ?
                        }
                    }
                    copy = new string(temp);
                }
                initial = copy;
            }

            return initial.Count(x => x == '#');
        }
    }
}
