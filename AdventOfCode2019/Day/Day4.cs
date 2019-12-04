using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Day
{
    class Day4
    {
        private string _path = "";
        public Day4(string path)
        {
            _path = Path.Combine(path, "InputDay4.txt");
            Console.WriteLine("Advent of Code Day 4 part 1 : Amount = " + PartOne());
            Console.WriteLine("Advent of Code Day 4 part 2 : Amount = " + PartTwo());
        }

        private int PartOne()
        {
            var min = int.Parse(File.ReadAllText(_path).Substring(0, 6));
            var max = int.Parse(File.ReadAllText(_path).Substring(7));
            var count = 0;

            for (int i = min; i <= max; i++)
            {
                if (CheckValidity(i))
                    count++;
            }
            return count;
        }

        private bool CheckValidity(int val)
        {
            var hasDouble = false;
            var test = val.ToString();
            for (int i = 0; i < (test.Length - 1); i++)
            {
                if (test[i] > test[i+1])
                {
                    return false;
                }
                hasDouble = hasDouble || test[i + 1] == test[i];
            }
            return hasDouble;
        }

        private int PartTwo()
        {
            var min = int.Parse(File.ReadAllText(_path).Substring(0, 6));
            var max = int.Parse(File.ReadAllText(_path).Substring(7));
            var count = 0;

            for (int i = min; i <= max; i++)
            {
                if (CheckValidityExtended(i))
                    count++;
            }
            return count;
        }

        private bool CheckValidityExtended(int val)
        {
            var test = val.ToString();
            var duplicates = new Dictionary<char, int>();

            for (int i = 0; i < (test.Length - 1); i++)
            {
                if (test[i] > test[i + 1])
                {
                    return false;
                }

                if (test[i + 1] == test[i])
                {
                    if (!duplicates.TryAdd(test[i],2))
                    {
                        duplicates[test[i]]++;
                    }
                }
            }
            return duplicates.ContainsValue(2);
        }
    }
}
