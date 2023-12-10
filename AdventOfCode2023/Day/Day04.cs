using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        var lines = File.ReadAllLines(_path)
            .Select(x => x.Split(':')[1].Split('|').ToList());
        
        var count = 0;
        foreach (var line in lines) 
        {
            var first = line[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            var second = line[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            var test = first.Intersect(second).ToList();

            if (test.Any())
                count += (int)Math.Pow(2,(test.Count-1));
        }
        Console.WriteLine(count);
    }

    private void PartTwo()
    {
        List<(int count, List<string> data)> lines = File.ReadAllLines(_path)
            .Select(x => (1,x.Split(':')[1].Split('|').ToList())).ToList();

        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            var linecount = line.count;

            var first = line.data[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            var second = line.data[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            var matches = first.Intersect(second).ToList();
            for (int k = 1; k <= matches.Count; k++)
            {
                if (i + k > lines.Count)
                    break;

                var temp = lines[i + k];
                lines[i + k] = (temp.count + linecount, temp.data);
            }
        }
        var sum = lines.Sum(x => x.count);
        Console.WriteLine(sum);
    }
}
