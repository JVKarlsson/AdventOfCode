
namespace AdventOfCode2024.Day;
internal class Day02
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
        var result = File.ReadAllLines(_path)
                .Select(x => 
                    x.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(y => int.Parse(y))
                    .ToList())
                .ToList();

        var safeCount = 0;
        foreach (var line in result)
        {
            safeCount += IsSafe(line, false);
        }
        Console.WriteLine(safeCount);
    }

    private void PartTwo()
    {
        var result = File.ReadAllLines(_path)
                .Select(x =>
                    x.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(y => int.Parse(y))
                    .ToList())
                .ToList();

        var safeCount = 0;
        foreach (var line in result)
        {
            safeCount += IsSafe(line, true);
        }
        Console.WriteLine(safeCount);
    }

    private int IsSafe(List<int> line, bool dampenerActivated)
    {
        bool isAscending = (line[0] - line[1]) < 0;

        for (int i = 0; i < line.Count-1; i++)
        {
            var diff = line[i] - line[i+1];
            if (Math.Abs(diff) > 3 || diff == 0)
            {
                if (!dampenerActivated)
                    return 0;

                return IsSafeWithRemovedIndex(line, i);
            }

            if ((isAscending ^ diff < 0))
            {
                if (!dampenerActivated)
                    return 0;

                return IsSafeWithRemovedIndex(line, i);
            }
        }

        return 1;
    }

    private int IsSafeWithRemovedIndex(List<int> line, int i)
    {
        var start = Math.Max(0, i-1);
        for (int j = start; j <= i + 1; j++)
        {
            var copy = line.Select(x => x).ToList();
            copy.RemoveAt(j);
            if (IsSafe(copy, false) == 1)
                return 1;
        }
        return 0;
    }
}
