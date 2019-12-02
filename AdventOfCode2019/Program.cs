using AdventOfCode2019.Day;
using System;
using System.IO;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetFullPath(@"..\..\..\"), "Inputs");
            //_ = new Day1(path);
            _ = new Day2(path);

            Console.ReadLine();
        }
    }
}
