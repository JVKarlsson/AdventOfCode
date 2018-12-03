using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day
{
    class Day1
    {
        private string path = "";
        private List<int> pastFrequencies = new List<int> { 0 };
        private int frequency = 0;

        public Day1(string p)
        {
            path = Path.Combine(p, "InputDay1.txt");
            Console.WriteLine("Advent of Code Day 1 part 1 : Frequency = " + PartOne());
            Console.WriteLine("Advent of Code Day 1 part 2 : First frequency = " + PartTwo());
        }

        private int PartOne()
        {
            using (var sr = new StreamReader(path))
            {
                var line = "";
                while ((line = sr.ReadLine()) != null)
                    frequency += Convert.ToInt32(line);
            }
            return frequency;
        }

        private int PartTwo()
        {
            frequency = 0;
            while (!FindRepeat());
            return frequency;
        }

        private bool FindRepeat()
        {
            using (var sr = new StreamReader(path))
            {
                var line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    frequency += Convert.ToInt32(line);
                    if (pastFrequencies.Contains(frequency))
                        return true;
                    pastFrequencies.Add(frequency);
                }
            }
            return false;
        }
    }
}
