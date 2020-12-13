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
                                      .Select(Id => (Id, Wait : Id - (time % Id)))
                                      .OrderBy(x => x.Wait)
                                      .Select(x => x.Id * x.Wait)
                                      .First();

            Console.WriteLine($"Advent of Code Day 13 part 1 : {minimum}");
        }

        public void PartTwo()
        {
            var input = File.ReadAllLines(_path)[1].Replace("x,", "1,").Split(",")
                .Select(x => long.Parse(x)).ToArray();


            bool asd = true;
            var start = input[0];
            var multiple = 0;
            var values = input[1..];
            while(asd)
            {
                multiple++;
                var compare = start * multiple;
                var modulus = input.Select((x, i) => (compare + i) % x == 0).ToArray();

                if (!modulus.Any(x => !x))
                {
                    asd = false;
                }
            }
            var test = start * multiple;
            Console.WriteLine($"Advent of Code Day 13 part 2 : {0}");
        }
    }
}
