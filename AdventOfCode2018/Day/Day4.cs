using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day
{
    class Day4
    {
        private string path = "";

        public Day4(string p)
        {
            path = Path.Combine(p, "InputDay4.txt");
            Console.WriteLine("Advent of Code Day 4 part 1 :  = " + PartOne());
            //Console.WriteLine("Advent of Code Day 4 part 2 :  = " + PartTwo());
        }

        private int PartOne()
        {
            int[] minute = new int[60];
            List<int> Guard = new List<int>();
            List<int> month = new List<int>();
            List<int> day = new List<int>();


            var lines = File.ReadAllLines(path).ToList();
            var GuardId = 0;
            int fellAsleep = 0;
            int wokeup = 0;
            foreach (var line in lines)
            {
                var date = line.Substring(1, 16); // substring 1,16
                if (line.Contains("Guard"))
                {
                    var start = line.IndexOf('#');
                    var spaceindex = line.IndexOf(" ", start);
                    GuardId = Convert.ToInt32(line.Substring(start + 1, (spaceindex - start)));

                    if (Guard.Contains(GuardId))
                    {
                        Guard.Add(GuardId);
                    }


                }
                else if (line.Contains("wakes"))
                {

                }
                else if (line.Contains("falls"))
                {

                }

            }

            return GuardId * 0;
        }

        private int PartTwo()
        {
            var counter = 0;

            return counter;
        }



    }
}
