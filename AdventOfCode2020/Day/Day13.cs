using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    class Day13
    {
        private string _path;
        public Day13(string path)
        {
            _path = Path.Combine(path, "InputDay13.txt");

            PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var input = File.ReadAllLines(_path).ToList();

            var time = int.Parse(input[0]);
            var minimum = input[1].Replace("x,", "")
                                      .Split(",")
                                      .Select(x => int.Parse(x))
                                      .Select(Id => (Id, Wait: Id - (time % Id)))
                                      .OrderBy(x => x.Wait)
                                      .Select(x => x.Id * x.Wait)
                                      .First();

            Console.WriteLine($"Advent of Code Day 13 part 1 : {minimum}");
        }

        public void PartTwo()
        {
            var input = File.ReadAllLines(_path)[1].Replace("x,", "1,").Split(",")
                .Select(x => long.Parse(x)).ToArray();

            long starttime = 0;
            long step = input[0];
            for (int i = 1; i < input.Length; i++)
            {
                while (true && input[i] != 1)
                {
                    starttime += step;
                    var modulus = (starttime + i) % input[i] == 0;
                    if (modulus)
                    {
                        step *= input[i];
                        break;
                    }
                }
            }
            Console.WriteLine($"Advent of Code Day 13 part 2 : {starttime}");
        }
    }
}
