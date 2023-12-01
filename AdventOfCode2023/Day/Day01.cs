using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day;
public class Day01
{
    private readonly string _path;
    public Day01(string path)
    {
        _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
        PartOne();
        PartTwo();
    }
    private void PartOne()
    {
        var result = File.ReadAllLines(_path)
                .Select(x => x.ToArray())
                .Select(x => x.Where(y => int.TryParse(y.ToString(), out _)).ToList())
                .Select(x => x[0].ToString() + x[^1].ToString())
                .Sum(x => int.Parse(x));

        Console.WriteLine(result);
    }

    private void PartTwo()
    {
        var match = @"(?=(\d|one|two|three|four|five|six|seven|eight|nine))";
        var result = File.ReadAllLines(_path)
                .Select(x => Regex.Matches(x, match)
                    .Select(y => y.Groups[1].ToString()).ToList())
                .Select(x => x.Select(y => GetAsNumberFormat(y)).ToList())
                .Select(x => x[0] + x[^1])
                .ToList();
        var sum = result.Sum(x => int.Parse(x));
        Console.WriteLine(result);
    }

    private static string GetAsNumberFormat(string value) => value switch
    {
        "one" => "1",
        "two" => "2",
        "three" => "3",
        "four" => "4",
        "five" => "5",
        "six" => "6",
        "seven" => "7",
        "eight" => "8",   
        "nine" => "9",
        _ => value
    };
}
