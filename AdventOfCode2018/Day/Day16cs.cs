using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day
{
    class Day16
    {
        private string path = "";
        public Day16(string p)
        {
            path = Path.Combine(p, "InputDay16.txt");
            //Console.WriteLine("Advent of Code Day 16 part 1 " + PartOne());
            //Console.WriteLine("Advent of Code Day 16 part 2 " + PartTwo());
        }
    }

    // 4 registers 0-3 starts with value 0
    // every instruction = opcode, 2 inputs, output
    // opcode => how inputs are interpreted, output is always treated as register
}
