using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day;
public class Day04
{
    private readonly string _path;
    public Day04(string path)
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
