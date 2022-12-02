﻿using AdventOfCode2022.Day;

namespace AdventOfCode2022
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetFullPath(@"..\..\..\"), "Inputs");

            //_ = new Day01(path);
            _ = new Day02(path);

            Console.ReadLine();
        }
    }
}