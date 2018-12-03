using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day
{
    class Day3
    {
        private string path = "";
        private int[,] cloth = new int[1000, 1000];

        public Day3(string p)
        {
            path = Path.Combine(p, "InputDay3.txt");
            Console.WriteLine("Advent of Code Day 3 part 1 : square inches = " + PartOne());
            Console.WriteLine("Advent of Code Day 3 part 2 : id = " + PartTwo());
        }

        private int PartOne()
        {
            int sqri = 0;
            var lines = File.ReadAllLines(path).ToList();

            foreach (var line in lines)
            {
                var lineSplit = line.Split('@', ',', ':','x');
                var id = Convert.ToInt32(lineSplit[0].Substring(1).Trim());
                var startPos = new int[] { Convert.ToInt32(lineSplit[1]) , Convert.ToInt32(lineSplit[2]) };
                var movement = new int[] { Convert.ToInt32(lineSplit[3]) , Convert.ToInt32(lineSplit[4]) };

                for (int i = startPos[0]; i < (startPos[0] + movement[0]); i++)
                {
                    for (int j = startPos[1]; j < (startPos[1] + movement[1]); j++)
                    {
                        if (cloth[i, j] == 0)
                            cloth[i, j]++;
                        else if(cloth[i, j] == 1)
                        {
                            cloth[i, j]++;
                            sqri++;
                        }
                    }
                }
            }

            return sqri;
        }

        private int PartTwo()
        {
            var lines = File.ReadAllLines(path).ToList();

            foreach (var line in lines)
            {
                var lineSplit = line.Split('@', ',', ':', 'x');
                var id = Convert.ToInt32(lineSplit[0].Substring(1).Trim());
                var startPos = new int[] { Convert.ToInt32(lineSplit[1]), Convert.ToInt32(lineSplit[2]) };
                var movement = new int[] { Convert.ToInt32(lineSplit[3]), Convert.ToInt32(lineSplit[4]) };
                bool noOverlap = true;

                for (int i = startPos[0]; i < (startPos[0] + movement[0]); i++)
                {
                    for (int j = startPos[1]; j < (startPos[1] + movement[1]); j++)
                    {
                        if (cloth[i, j] != 1)
                            noOverlap = false;
                    }
                }
                if (noOverlap)
                    return id;
            }

            return -1;
        }
    }
}
