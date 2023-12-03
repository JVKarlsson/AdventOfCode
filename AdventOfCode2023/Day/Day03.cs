using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day;
public class Day03
{
    private readonly string _path;
    public Day03(string path)
    {
        _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
        PartOne();
        PartTwo();
    }

    private void PartOne()
    {
        var lines = File.ReadAllLines(_path)
                .Select(x => x.ToArray())
                .ToArray();

        List<int> numbers = [];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            List<char> numberCombination = [];
            bool lastWasNumber = false;
            bool hasNeighbour = false;

            for (int j = 0; j < line.Length; j++)
            {
                var isnum = int.TryParse(line[j].ToString(), out int number);

                if (isnum)
                {
                    numberCombination.Add((line[j]));

                    var neigh = CheckForNeighbourWithSign(lines, (i, j));
                    hasNeighbour |= neigh;

                    lastWasNumber = true;
                }
                else if (lastWasNumber)
                {
                    if (hasNeighbour)
                    {
                        numbers.Add(int.Parse((string.Concat(numberCombination))));
                    }

                    numberCombination = [];
                    lastWasNumber = false;
                    hasNeighbour = false;
                }
            }
        }
        var sum = numbers.Sum();
        Console.WriteLine(sum);
    }

    private bool CheckForNeighbourWithSign(char[][] lines, (int i, int j) pos)
    {
        for (int i = -1; i < 2; i++)
        {
            var newi = pos.i + i;
            if (newi < 0 || newi >= lines.Length)
                continue;

            for (int j = -1; j < 2; j++)
            {
                var newj = pos.j + j;
                if (newj < 0 || newj >= lines[0].Length)
                    continue;


                if (lines[newi][newj] != '.' &&
                    !char.IsNumber(lines[newi][newj]))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void PartTwo()
    {
        var lines = File.ReadAllLines(_path)
                .Select(x => x.ToArray())
                .ToArray();

        Dictionary<(int i, int j), List<int>> allMatches = [];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            Dictionary<(int i, int j), List<int>> matches = [];
            List<char> numberCombination = [];
            bool lastWasNumber = false;

            for (int j = 0; j < line.Length; j++)
            {
                var isnum = int.TryParse(line[j].ToString(), out int number);

                if (isnum)
                {
                    numberCombination.Add((line[j]));

                    var gears = CheckForNeighbouringGears(lines, (i, j));
                    if (gears.Count > 0)
                    {
                        foreach (var gear in gears)
                        {
                            matches.TryAdd(gear, []);
                        }
                    }

                    lastWasNumber = true;
                }
                else if (lastWasNumber)
                {
                    var value = int.Parse(string.Concat(numberCombination));
                    if (true)
                    {
                        foreach (var match in matches)
                        {
                            match.Value.Add(value);
                            allMatches.TryAdd(match.Key, []);
                            allMatches[match.Key].AddRange(match.Value);
                        }
                        matches = [];
                    }

                    numberCombination = [];
                    lastWasNumber = false;
                }
            }
        }
        
        var sum = allMatches.Where(match => match.Value.Count is 2).Select(x => x.Value[0] * x.Value[1]).Sum();
        Console.WriteLine(sum);
    }
    private List<(int i, int j)> CheckForNeighbouringGears(char[][] lines, (int i, int j) pos)
    {
        List<(int i, int j)> gears = [];
        for (int i = -1; i < 2; i++)
        {
            var newi = pos.i + i;
            if (newi < 0 || newi >= lines.Length)
                continue;

            for (int j = -1; j < 2; j++)
            {
                var newj = pos.j + j;
                if (newj < 0 || newj >= lines[0].Length)
                    continue;


                if (lines[newi][newj] == '*')
                {
                    gears.Add((newi, newj));
                }
            }
        }
        return gears;
    }
}
