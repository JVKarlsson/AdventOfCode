using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day
{
    class Day7
    {
        private static string path = "";
        public Day7(string p)
        {
            path = Path.Combine(p, "InputDay7.txt");
            var solution = Solve();
            Console.WriteLine("Advent of Code Day 7 part 1 : " + solution[0]);
            Console.WriteLine("Advent of Code Day 7 part 2 : " + solution[1]);
        }

        private int[] Solve()
        {
            var answer = 0;

            return new int[]{ answer, answer};
        }

        private string PartOne()
        {
            var order = "";

            return order;
        }
    }
}
