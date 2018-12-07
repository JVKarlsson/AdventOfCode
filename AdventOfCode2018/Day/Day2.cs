using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Day
{
    class Day2
    {
        private string path = "";
        private int OccuredTwice = 0;
        private int OccuredThrice = 0;
        private List<string> commonLines = new List<string>();

        public Day2(string p)
        {
            path = Path.Combine(p, "InputDay2.txt");
            Console.WriteLine("Advent of Code Day 2 part 1 : checksum = " + PartOne());
            Console.WriteLine("Advent of Code Day 2 part 2 : common letters = " + PartTwo());
        }

        private int PartOne()
        {
            using (var sr = new StreamReader(path))
            {
                var line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    var characters = line.ToCharArray().GroupBy(x => x);
                    if (characters.Any(y => y.Count() == 2))
                        OccuredTwice++;
                    if (characters.Any(y => y.Count() == 3))
                        OccuredThrice++;
                }
            }
            return OccuredThrice * OccuredTwice;
        }

        private string PartTwo()
        {
            var lines = File.ReadAllLines(path).ToList();
            foreach (var line in lines)
            {
                var lineChars = line.ToCharArray();
                foreach (var otherLine in lines)
                {
                    var otherChars = otherLine.ToCharArray();
                    var count = 0;
                    var index = 0;

                    for (int i = 0; i < lineChars.Length; i++)
                    {
                        if (lineChars[i] != otherChars[i])
                        {
                            count++;
                            index = i;
                        }

                        if (count > 1)
                            break;
                    }

                    if (count == 1)
                    {
                        var shortened = line.Remove(index , 1);
                        if (shortened == otherLine.Remove(index, 1))
                        {
                            commonLines.Add(shortened);
                        }
                    }
                }
            }
            return commonLines[0];
        }
    }
}
