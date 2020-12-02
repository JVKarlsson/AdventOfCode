﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day
{
    public class Day01
    {
        private string _path;
        public Day01(string path)
        {
            _path = Path.Combine(path, "InputDay01.txt"); ;
            PartOne();
            PartTwo();
        }

        public void PartOne()
        {
            var input = File.ReadAllLines(_path).Select(x => int.Parse(x)).ToList();

            // Cleaner but slower
            //var result = (from a in input
            //              from b in input
            //              where a + b == 2020
            //              select a * b).FirstOrDefault();


            // Faster but ugly
            var result = 0;
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = i + 1; j < input.Count; j++)
                {
                    if (input[i] + input[j] == 2020)
                    {
                        result = input[i] * input[j];
                        break;
                    }
                }
                if (result is not 0)
                    break;
            }
            Console.WriteLine($"Advent of Code Day 01 part 1 : {result}");
        }

        public void PartTwo()
        {
            var input = File.ReadAllLines(_path).Select(x => int.Parse(x)).ToList();
            var result = (from a in input
                          from b in input
                          from c in input
                          where (a + b + c) == 2020
                          select (a * b * c)).FirstOrDefault();

            Console.WriteLine($"Advent of Code Day 01 part 2 : {result}");
        }

    }
}
