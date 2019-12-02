using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Day
{
    class Day1
    {
        private string _path = "";
        public Day1(string path)
        {
            _path = Path.Combine(path, "InputDay1.txt");
            Console.WriteLine("Advent of Code Day 1 part 1 : Fuel Sum = " + PartOne());
            Console.WriteLine("Advent of Code Day 1 part 2 : Fuel Sum = " + PartTwo());
        }

        private int PartOne()
        {
            var sum = File.ReadAllLines(_path).ToList().Sum(x => (int.Parse(x) / 3) - 2);
            return sum;
        }

        private int PartTwo()
        {
            var sum = File.ReadAllLines(_path).ToList().Sum(x => GetFuelAndSubFuel(int.Parse(x)));
            return sum;
        }

        private int GetFuelAndSubFuel(int fuel)
        {
            var amount = Math.Max(0, ((fuel / 3) - 2));
            if (amount > 0)
            {
                amount += GetFuelAndSubFuel(amount);
            }
            return amount;
        }

    }
}
