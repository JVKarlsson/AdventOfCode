using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day;
public class Day02
{
    private readonly string _path;
    public Day02(string path)
    {
        _path = Path.Combine(path, $"Input{this.GetType().Name}.txt");
        PartOne();
        PartTwo();
    }

    private void PartOne()
    {
        var games = File.ReadAllLines(_path)
            .Select(x => x.Split(':'))
            .ToDictionary(
                x => x[0].Replace("Game ", ""), 
                x => x[1].Split(';').Select(y => y.Trim()).ToList());

        List<int> ids = [];
        foreach (var game in games)
        {
            var success = true;
            foreach (var item in game.Value)
            {
                var balls = new Dictionary<string, int>
                {
                    { "red", 12 },
                    { "green", 13 },
                    { "blue", 14 }
                };
                var lost = item
                    .Split(",")
                    .Select(x => x.Trim().Split(" "))
                    .ToList();

                foreach (var color in lost)
                {
                    balls[color[1]] -= int.Parse(color[0]);
                }

                if (balls.Any(x => x.Value < 0))
                {
                    success = false;
                    break;
                }

            }
            if (success)
                ids.Add(int.Parse(game.Key));
        }
        var sum = ids.Sum();
        Console.WriteLine(sum);
    }



    private void PartTwo()
    {
        var games = File.ReadAllLines(_path)
            .Select(x => x.Split(':'))
            .ToDictionary(
                x => x[0].Replace("Game ", ""),
                x => x[1].Split(';').Select(y => y.Trim()).ToList());

        List<int> ids = [];
        foreach (var game in games)
        {
            var balls = new Dictionary<string, int>
                {
                    { "red", 0 },
                    { "green", 0 },
                    { "blue", 0 }
                };
            foreach (var item in game.Value)
            {
                var lost = item
                    .Split(",")
                    .Select(x => x.Trim().Split(" "))
                    .ToList();

                foreach (var color in lost)
                {
                    balls[color[1]]  = Math.Max(int.Parse(color[0]), balls[color[1]]);
                }
            }
            var pow = balls.Select(x => x.Value).ToList();
            ids.Add(pow[0] * pow[1] * pow[2]);
        }


        var sum = ids.Sum();
        Console.WriteLine(sum);
    }
}
