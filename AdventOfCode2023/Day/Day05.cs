using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day;
public class Day05
{
    private readonly string _path;
    public Day05(string path)
    {
        _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
        PartOne();
        PartTwo();
    }

    private void PartOne()
    {
        var lines = File.ReadAllLines(_path);
        Console.WriteLine(1);
    }

    private void PartTwo()
    {
        var lines = File.ReadAllLines(_path);
        Console.WriteLine(1);
    }
}
