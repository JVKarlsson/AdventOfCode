using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day;
public class Day06
{
    private readonly string _path;
    public Day06(string path)
    {
        _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
        PartOne();
        PartTwo();
    }

    private void PartOne()
    {
        var lines = File.ReadAllLines(_path);
        var times = lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
        var distances = lines[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();

        var numbers = new List<int>();
        for (int i = 0; i < times.Count; i++)
        {
            var time = times[i];
            var distance = distances[i];

            var numOfSolutions = 0;
            var started = false;
            for (int j = 1; j < time; j++)
            {
                var runtime = time - j;
                var newDistance = runtime * j;
                if (distance < newDistance)
                {
                    numOfSolutions++;
                    started = true;
                }
                else if (started)
                {
                    break;
                }
            }
            numbers.Add(numOfSolutions);
        }

        var result = numbers.Aggregate((a, x) => a * x);
        Console.WriteLine(result);
    }

    private void PartTwo()
    {
        var lines = File.ReadAllLines(_path);
        var time = double.Parse(lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Aggregate((a, x) => a + x));
        var distance = double.Parse(lines[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Aggregate((a, x) => a + x));

        var numOfSolutions = 0d;
        var started = false;
        for (int j = 1; j < time; j++)
        {
            var runtime = time - j;
            var newDistance = runtime * j;
            if (distance < newDistance)
            {
                numOfSolutions++;
                started = true;
            }
            else if (started)
            {
                break;
            }
        }
        Console.WriteLine(numOfSolutions);
    }
}
